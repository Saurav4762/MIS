using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;
using MIS.API.Models;

namespace MIS.API.DTOs;


public record OptionItemRequestDTO
{
  public string Code { get; set; } = null!;
  public string LabelEn { get; set; } = null!;
  public string LabelNe { get; set; } = null!;

  public Dictionary<string, object>? Extra { get; set; }
  public int? SortOrder { get; set; }

};

public record OptionItemsRequestDTO
{

  public Guid OptionListId { get; set; }

  public List<OptionItemRequestDTO> Items { get; set; } = null!;

}



public record OptionItemResponseDTO
{
  public Guid Id { get; set; }
  public string Code { get; set; } = null!;
  public string LabelEn { get; set; } = null!;
  public string LabelNe { get; set; } = null!;

  public Dictionary<string, object>? Extra { get; set; }
  public int? SortOrder { get; set; }
};


public record OptionItemsResponseDTO
{
  public Guid OptionListId { get; set; }
  public List<OptionItemResponseDTO> Items { get; set; } = null!;

}