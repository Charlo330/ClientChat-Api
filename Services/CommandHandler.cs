using chatApi.Commands;
using chatApi.Managers;
using chatApi.Models;

namespace chatApi.Services;

public class CommandHandler
{
    private Dictionary<string, ICommand> _commands;

    public CommandHandler(RoomService roomService)
    {
        _commands = new Dictionary<string, ICommand>()
        {
            { "ROOM SEND", new RoomSendMessageCommand(roomService) },
        };
    }

    public void Handle(MessageData messageData)
    {
        if (_commands.TryGetValue(messageData.Command, out ICommand? command))
        {
            command.Execute(messageData);
        }
    }
}