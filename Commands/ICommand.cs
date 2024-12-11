using chatApi.Models;

namespace chatApi.Commands;

public interface ICommand
{
    void Execute(MessageData messageData);
}