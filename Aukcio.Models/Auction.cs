using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Aukcio.Models
{
    public class Auction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string UId { get; set; }

        public Auction()
        {
            this.Images = new List<AuctionImage>();
        }

        public int? BuyoutPrice { get; set; }
        [Required]
        public int CurrentBid { get; set; }
        [Required]
        [MaxLength(255)]
        public string AuctionName { get; set; }
        [Required]
        [MaxLength(1024)]
        public string Description { get; set; }
        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<AuctionImage> Images { get; set; }
        
        [JsonIgnore]
        [NotMapped]
        public virtual AuctionUser Owner { get; set; }
        public Guid OwnerId { get; set; }

    }

    public class AuctionImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string UId { get; set; }
        
        public string AuctionId { get; set; }
        
        public virtual Auction Auction { get; set; }

        private byte[] ImageData { get; set; }
    }
}