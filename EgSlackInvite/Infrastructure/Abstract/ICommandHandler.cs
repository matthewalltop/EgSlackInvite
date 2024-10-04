namespace EgSlackInvite.Infrastructure.Abstract
{
    using Models;

    public interface ICommandHandler
    {
        /// <summary>
        /// Parses a text command into a set of parameters
        /// </summary>
        /// <param name="commandText">The <see cref="string"/> text to parse</param>
        /// <returns>A <see cref="Command"/> object.</returns>
        Command ParseCommand(string commandText);
    }
}
