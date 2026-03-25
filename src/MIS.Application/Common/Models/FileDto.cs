namespace MIS.Application.Common.Models;

public class FileDto
{
  required public string FileName { get; init; } = "";
  required public string ContentType { get; init; }
  public long SizeInBytes { get; init; }
  required public Stream Content { get; init; }
}