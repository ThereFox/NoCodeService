namespace NodeBuilder.DTOs;

public record NodeConfigInputObject( 
    int Id,
    int TypeId,
    List<int> ConnectedElements,
    string Configuration
);