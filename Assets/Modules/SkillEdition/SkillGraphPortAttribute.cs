[System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false)]  
public class SkillGraphPortAttribute : System.Attribute  
{
    public enum Direction {Input, Output};
    public Direction direction;

    public SkillGraphPortAttribute(Direction direction)  
    {  
        this.direction = direction;
    }  
} 