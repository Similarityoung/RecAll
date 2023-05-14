using System.ComponentModel.DataAnnotations;

namespace RecAll.Contrib.MaskedTextList.Api.Commands;

public class CreateMaskedTextItemCommand {
    [Required]
    public string Content { get; set; }
    public string MaskedContent { get; set; }
}