namespace NodeBuilder.Attributes;

public class ActionCodeAttribute : Attribute
{
    public ActionCodeAttribute(int id) 
    {
        Id = id;
    }
    
    public int Id { get; init; }
}