using System.Windows.Input;
using chatApi.Models;
using chatApi.Services;

namespace chatApi.Commands;

public class RoomSendMessageCommand : ICommand
{
    private readonly RoomService _roomService;

    public RoomSendMessageCommand(RoomService roomService)
    {
        _roomService = roomService;
    }

    public void Execute(MessageData messageData)
    {
        if (messageData.RoomId is null)
        {
            throw new ArgumentNullException(nameof(messageData.RoomId), "RoomId cannot be null.");
        }

        if (messageData.Message is null)
        {
            throw new ArgumentNullException(nameof(messageData.Message), "Message cannot be null.");
        }
        
        _roomService.SendMessageToRoom(messageData.UserId, messageData.RoomId.Value, messageData.Message);
    }
}