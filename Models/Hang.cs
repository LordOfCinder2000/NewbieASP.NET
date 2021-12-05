namespace FShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Hang")]
    public partial class Hang
    {
        [Key]
        [StringLength(10)]
        [Required(ErrorMessage = "Mã hàng không được để trống!")]
        [DisplayName("Mã hàng")]
        public string MaHang { get; set; }

        [Required(ErrorMessage = "Mã nhà cung cấp không được để trống!")]
        [DisplayName("Nhà cung cấp")]
        [StringLength(10)]
        
        public string MaNCC { get; set; }

        [Required(ErrorMessage = "Tên hàng không được để trống!")]
        [DisplayName("Tên hàng")]
        [StringLength(255)]
        public string TenHang { get; set; }

        [DisplayName("Giá")]
        public decimal? Gia { get; set; }

        [Required(ErrorMessage = "Lượng có không được để trống!")]
        [DisplayName("Lượng có")]
        public decimal LuongCo { get; set; }

        [StringLength(1000)]
        [DisplayName("Mô tả")]
        public string MoTa { get; set; }

        [DisplayName("Chiết khấu")]
        public decimal? ChietKhau { get; set; }

        [StringLength(100)]
        [DisplayName("Hình ảnh")]
        public string HinhAnh { get; set; }

        public virtual Nha_CC Nha_CC { get; set; }
    }
}
