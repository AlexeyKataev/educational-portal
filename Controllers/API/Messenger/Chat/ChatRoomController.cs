using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dotnet.Models;
using Dotnet.Enums.API;
using Dotnet.ViewModels.API.Messenger.Chat;
using Dotnet.ViewModels.API.Education;

namespace Dotnet.Controllers.API.Messenger.Chat
{
	[Route("API/Messenger/Chat/ChatRoom")]
	[ApiController]
	public class ChatRoomController : ControllerBase
	{
		private ApplicationContext _context;

        public ChatRoomController(ApplicationContext context)
        {
            _context = context;
        }

		[HttpPost]
		public async Task<ActionResult<ChatRoomViewModel>> PostChatRoom(StudentViewModel student) 
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
					// Поиск получателя
					Dotnet.Models.Student companionAsStudent = await _context.Students.FirstOrDefaultAsync(x => x.Id == student.Id);
					Dotnet.Models.User companion = await _context.Users.FirstOrDefaultAsync(x => x.Id == companionAsStudent.UserId);

					if (companion == null) 
					{
						return BadRequest();
					}

					// Получение сводных записей с получателем
					List<Dotnet.Models.Messenger.Chat.UserChatRoom> companionUserChatRoomCheck = await _context.UserChatRoom.Where(x => x.UserId == companion.Id).ToListAsync();
					
					
					// Получение сводных записей с отправителем
					List<Dotnet.Models.Messenger.Chat.UserChatRoom> senderUserChatRoomCheck = await _context.UserChatRoom.Where(x => x.UserId == userCheck.Id).ToListAsync();


					if (companionUserChatRoomCheck != null && senderUserChatRoomCheck != null)
					{
						foreach (var s in senderUserChatRoomCheck)
						{
							foreach (var c in companionUserChatRoomCheck)
							{
								if (c.ChatRoomId == s.ChatRoomId)
								{
									Dotnet.Models.Messenger.Chat.ChatRoom chatRoom = await _context.ChatRooms.FirstOrDefaultAsync(x => x.Id == c.ChatRoomId);

									return new ChatRoomViewModel {
										Id				= chatRoom.Id,
										Name			= chatRoom.Name,
										DateOfCreation	= chatRoom.DateOfCreation,
									};
								}
							}
						}
					}
					else 
					{
						return new ChatRoomViewModel();
					}
				}
			}

			return BadRequest();
		}
	}
}