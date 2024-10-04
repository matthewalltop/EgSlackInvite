namespace EgSlackInvite.Infrastructure.Concrete
{
    using Abstract;
    using Models;

    public class CommandHandler: ICommandHandler
    {
        /// <inheritdoc cref="ICommandHandler.ParseCommand"/>
        public Command ParseCommand(string commandText) 
            => throw new System.NotImplementedException();
    }
}
