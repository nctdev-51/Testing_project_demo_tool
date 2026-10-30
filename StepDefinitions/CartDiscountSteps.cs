using System;
using BasicExercises.Exercises;
using Reqnroll;
using Xunit;

namespace BasicExercises.StepDefinitions;

[Binding]
public class CartDiscountSteps
{
    private readonly CartService _cartService = new();
    private decimal _manualOrderTotal = 0;
    private DiscountResult _lastResult = new();

    [Given("Khách hàng là thành viên {string}")]
    public void GivenKhachHangLaThanhVien(string tier)
    {
        _cartService.CustomerTier = tier;
    }

    [Given("Giỏ hàng hiện đang trống")]
    public void GivenGioHangHienDangTrong()
    {
        _cartService.Clear();
    }

    [When("Khách hàng thêm danh sách sản phẩm sau vào giỏ:")]
    public void WhenKhachHangThemDanhSachSanPhamSauVaoGio(DataTable dataTable)
    {
        foreach (var row in dataTable.Rows)
        {
            string tenSP = row["TenSanPham"];
            int soLuong = int.Parse(row["SoLuong"]);
            decimal donGia = decimal.Parse(row["DonGia"]);

            _cartService.AddItem(tenSP, soLuong, donGia);
        }
    }

    [Then("Tổng tiền tạm tính phải là {decimal} VND")]
    public void ThenTongTienTamTinhPhaiLa(decimal expectedTotal)
    {
        decimal actualTotal = _cartService.GetSubTotal();
        Assert.Equal(expectedTotal, actualTotal);
    }

    [Given("Khách hàng có hạng thành viên là {string}")]
    public void GivenKhachHangCoHangThanhVienLa(string tier)
    {
        _cartService.CustomerTier = tier;
    }

    [Given("Đơn hàng có tổng trị giá là {decimal} VND")]
    public void GivenDonHangCoTongTriGiaLa(decimal total)
    {
        _manualOrderTotal = total;
    }

    [When("Khách hàng áp dụng mã giảm giá {string}")]
    public void WhenKhachHangApDungMaGiamGia(string voucherCode)
    {
        _lastResult = _cartService.ApplyDiscount(voucherCode, _manualOrderTotal > 0 ? _manualOrderTotal : null);
    }

    [Then("Số tiền được giảm phải là {decimal} VND")]
    public void ThenSoTienDuocGiamPhaiLa(decimal expectedDiscount)
    {
        Assert.Equal(expectedDiscount, _lastResult.DiscountAmount);
    }

    [Then("Số tiền thực trả là {decimal} VND")]
    public void ThenSoTienThucTraLa(decimal expectedFinal)
    {
        Assert.Equal(expectedFinal, _lastResult.FinalAmount);
    }

    [When("Khách hàng áp dụng mã giảm giá {string} đã hết hạn")]
    public void WhenKhachHangApDungMaGiamGiaDaHetHan(string voucherCode)
    {
        _lastResult = _cartService.ApplyDiscount(voucherCode, _manualOrderTotal > 0 ? _manualOrderTotal : null);
    }

    [Then("Hệ thống phải báo lỗi {string}")]
    public void ThenHeThongPhaiBaoLoi(string expectedErrorMessage)
    {
        Assert.False(_lastResult.IsSuccess);
        Assert.Equal(expectedErrorMessage, _lastResult.ErrorMessage);
    }

    [Then("Số tiền thực trả giữ nguyên là {decimal} VND")]
    public void ThenSoTienThucTraGiuNguyenLa(decimal expectedFinal)
    {
        Assert.Equal(expectedFinal, _lastResult.FinalAmount);
    }
}

