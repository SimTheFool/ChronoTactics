using System.Collections.Generic;

public static class SkillCommandRegistry
{
    public static Dictionary<string, SkillCommand> skillCommands;

    static SkillCommandRegistry()
    {
        skillCommands = new Dictionary<string, SkillCommand>();
        skillCommands[""] = null;
        skillCommands["End Turn"] = new EndTurnCommand();
        skillCommands["Move"] = new MoveCommand();
        skillCommands["ReceiveDamage"] = new ReceiveDamageCommand();
    }
}