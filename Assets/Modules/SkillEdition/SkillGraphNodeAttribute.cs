[System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = false)]  
public class SkillGraphNodeAttribute : System.Attribute  
{   
    public enum NodeTag {Default, Move, Other};
    public NodeTag nodeTag;

    public SkillGraphNodeAttribute(NodeTag nodeTag = NodeTag.Default)  
    {  
        this.nodeTag = nodeTag;
    }  
} 