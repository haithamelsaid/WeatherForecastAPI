using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WeatherForecast.Domain.Weathers
{
    public class Weather
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(25)")]
        public string City { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(25)")]
        public string WeatherForecast { get; set; }

        public int HighTemp { get; set; }
        public int LowTemp { get; set; }
    }
}
