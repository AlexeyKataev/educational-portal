using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dotnet.Models;
using Dotnet.Enums.API;
using Dotnet.ViewModels.API.Messenger.Chat;

namespace Dotnet.Controllers.API.Messenger.Chat
{
	[Route("API/Messenger/Chat/Message")]
	[ApiController]
	public class MessageController : ControllerBase
	{
		private ApplicationContext _context;

        public MessageController(ApplicationContext context)
        {
            _context = context;
        }

		[HttpPost]
		public async Task<ActionResult<UserMessageViewModel>> PostMessage(UserMessageViewModel userMessage) 
		{
			if (((EnumActions)Enum.Parse(typeof(EnumActions), 
				$"{Request.Headers["Action"].ToString()}", true) == EnumActions.Add))
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
					// Поиск чата, в которое отправляется данное сообщение
					Dotnet.Models.Messenger.Chat.ChatRoom chatRoomCheck = await _context.ChatRooms.FirstOrDefaultAsync(x => x.Id == userMessage.ChatRoom.Id);

					// Если чат не найден, а требуется отправить тет-а-тет сообщение - создать чат-комнату, добавить пользователей в сводную таблицу
					if (chatRoomCheck == null && userMessage.ChatRoom.Members.Count == 1)
					{
						// Проверить, существует ли собеседник
						Dotnet.Models.User memberCheck = await _context.Users.FirstOrDefaultAsync(x => x.Id == userMessage.ChatRoom.Members[0].Id);

						// Если собеседника не существует
						if (memberCheck == null)
						{
							return BadRequest();
						}

						// Создание чата
						Dotnet.Models.Messenger.Chat.ChatRoom newChatRoom = new Models.Messenger.Chat.ChatRoom { UserCreaterId = userCheck.Id };
						await _context.ChatRooms.AddAsync(newChatRoom);
						await _context.SaveChangesAsync();

						// Добавление пользователей в чат
						Dotnet.Models.Messenger.Chat.UserChatRoom newCreator = new Models.Messenger.Chat.UserChatRoom { UserId = userCheck.Id, ChatRoomId = newChatRoom.Id };
						await _context.UserChatRoom.AddAsync(newCreator);
						
						Dotnet.Models.Messenger.Chat.UserChatRoom newMember = new Models.Messenger.Chat.UserChatRoom { UserId = memberCheck.Id, ChatRoomId = newChatRoom.Id };
						await _context.UserChatRoom.AddAsync(newMember);

						await _context.SaveChangesAsync();

						// Создание нового сообщения
						Dotnet.Models.Messenger.Chat.UserMessage newMessage = new Models.Messenger.Chat.UserMessage { UserId = userCheck.Id, Messsage = userMessage.Message };
						await _context.UserMessages.AddAsync(newMessage);
						await _context.SaveChangesAsync();


						// Связывание чата и сообщения
						Dotnet.Models.Messenger.Chat.UserMessageChatRoom newUserMessageChatRoom = new Models.Messenger.Chat.UserMessageChatRoom { UserMessageId = newMessage.Id, ChatRoomId = newChatRoom.Id };
						await _context.UserMessageChatRoom.AddAsync(newUserMessageChatRoom);
						await _context.SaveChangesAsync();
					}
					else if (chatRoomCheck != null && userMessage.ChatRoom.Members.Count == 0)
					{
						// Проверить, существует ли собеседник
						Dotnet.Models.User companionCheck = await _context.Users.FirstOrDefaultAsync(x => x.Id == userMessage.ChatRoom.Members[0].Id);

						// Если собеседника не существует
						if (companionCheck == null)
						{
							return BadRequest();
						}

						// Проверить, входят ли соеседники в общий чат
						Dotnet.Models.Messenger.Chat.UserChatRoom senderInChatCheck = await _context.UserChatRoom.FirstOrDefaultAsync(x => x.UserId == userCheck.Id && x.ChatRoomId == chatRoomCheck.Id);
						Dotnet.Models.Messenger.Chat.UserChatRoom companionInChatCheck = await _context.UserChatRoom.FirstOrDefaultAsync(x => x.UserId == companionCheck.Id && x.ChatRoomId == chatRoomCheck.Id);

						if (senderInChatCheck != null && companionInChatCheck != null)
						{
							Dotnet.Models.Messenger.Chat.UserMessage newUserMessage = new Models.Messenger.Chat.UserMessage {
								Messsage 	= userMessage.Message,
								UserId		= userCheck.Id,
							};

							await _context.UserMessages.AddAsync(newUserMessage);

							Dotnet.Models.Messenger.Chat.UserMessageChatRoom newUserMessageChatRoom = new Models.Messenger.Chat.UserMessageChatRoom {
								UserMessageId	= newUserMessage.Id,
								ChatRoomId		= chatRoomCheck.Id,
							};

							await _context.UserMessageChatRoom.AddAsync(newUserMessageChatRoom);

							await _context.SaveChangesAsync();
						}
						else 
						{
							return BadRequest();
						}
					}
					else 
					{
						return BadRequest();
					}

					Dotnet.Models.Messenger.Chat.UserMessage sentUserMessage = new Models.Messenger.Chat.UserMessage {

					};

					return new UserMessageViewModel {

					};
				}
			}

			return BadRequest();
		}
	}
}