using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dotnet.Models;
using Dotnet.Enums.API;
using Dotnet.ViewModels.API.Messenger.Chat;

namespace Dotnet.Controllers.API.Messenger.Chat
{
	[Route("API/Messenger/Chat/Messages")]
	[ApiController]
	public class MessagesController : ControllerBase
	{
		private ApplicationContext _context;

        public MessagesController(ApplicationContext context)
        {
            _context = context;
        }

		[HttpPost]
		public async Task<ActionResult<List<UserMessageViewModel>>> PostMessages(ChatRoomViewModel chatRoom) 
		{
			if (((EnumActions)Enum.Parse(typeof(EnumActions), 
				$"{Request.Headers["Action"].ToString()}", true) == EnumActions.Get))
			{
				string token = Request.Headers["Token"].ToString();

				Models.Account.UserTicket userTicketCheck = await _context.UserTickets.FirstOrDefaultAsync(x => x.Token == token);
				Models.User userCheck = await _context.Users.FirstOrDefaultAsync(x => x.Id == userTicketCheck.UserId);

				// Если нет токена или пользователь не существует
				if (userTicketCheck == null || userCheck == null) return BadRequest();

				// Если токет деактивирован
				if (!userTicketCheck.IsValid) 
				{
					return BadRequest();
				}
				else 
				{
					userTicketCheck.LastActive = DateTime.UtcNow;
					await _context.SaveChangesAsync();
				}
				
				// Если пользователь - это студент
				if (userCheck.RoleId == 9)
				{
					// Поиск чата
					Dotnet.Models.Messenger.Chat.ChatRoom chatRoomCheck = await _context.ChatRooms.FirstOrDefaultAsync(x => x.Id == chatRoom.Id);

					// Если чат не найден
					if (chatRoomCheck == null)
					{
						return new List<UserMessageViewModel>();
					}
					else 
					{						
						// Получить все сводные записи с идентификатором чата
						List<Dotnet.Models.Messenger.Chat.UserMessageChatRoom> userMessageChatRoomCheck = await _context.UserMessageChatRoom.Where(x => x.ChatRoomId == chatRoomCheck.Id).ToListAsync();
						List<Dotnet.Models.Messenger.Chat.UserChatRoom> userChatRoomCheck = await _context.UserChatRoom.Where(x => x.ChatRoomId == chatRoomCheck.Id).AsNoTracking().ToListAsync();

						// Все сообщения из выбранного чата
						List<Dotnet.Models.Messenger.Chat.UserMessage> userMessagesCheck = new List<Models.Messenger.Chat.UserMessage>();

						foreach (var item in userMessageChatRoomCheck)
						{
							userMessagesCheck.Add(await _context.UserMessages.FirstOrDefaultAsync(x => x.Id == item.UserMessageId));
						}

						List<UserMessageViewModel> userMessages = new List<UserMessageViewModel>();

						if (userChatRoomCheck.Count() == 2)
						{
							foreach (var item in userMessagesCheck)
							{
								userMessages.Add(new UserMessageViewModel {
									Id			= item.Id,
									ChatRoom	= new ChatRoomViewModel {
										Id 			= chatRoomCheck.Id,
									},
									Message		= item.Messsage,
								});
							}
						}						
						
						return userMessages;
					}
				}
			}

			return BadRequest();
		}
	}
}