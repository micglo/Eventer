using System.ComponentModel.DataAnnotations;

namespace Eventer.Model.BindingModel.Client
{
    public class ResetClientSecretBindingModel
    {
        [Required]
        public string Id { get; set; }
    }
}