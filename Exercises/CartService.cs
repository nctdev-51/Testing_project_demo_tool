using System;
using System.Collections.Generic;
using System.Linq;

namespace BasicExercises.Exercises;

public class CartItem
{
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice => Quantity * UnitPrice;
}

public class Voucher
{
    public string Code { get; set; } = string.Empty;
    public decimal DiscountPercent { get; set; }
    public decimal MinOrderAmount { get; set; }
    public string? RequiredTier { get; set; }
    public bool IsExpired { get; set; }
}

public class DiscountResult
{
    public bool IsSuccess { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal FinalAmount { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
}

public class CartService
{
    public string CustomerTier { get; set; } = "Standard";
    public List<CartItem> Items { get; } = new();

    // Danh mục voucher cấu hình sẵn
    private readonly Dictionary<string, Voucher> _vouchers = new(StringComparer.OrdinalIgnoreCase)
    {
        { "SALE10", new Voucher { Code = "SALE10", DiscountPercent = 10, MinOrderAmount = 500000, RequiredTier = null, IsExpired = false } },
        { "VIP20", new Voucher { Code = "VIP20", DiscountPercent = 20, MinOrderAmount = 1000000, RequiredTier = "VIP", IsExpired = false } },
        { "EXPIRED99", new Voucher { Code = "EXPIRED99", DiscountPercent = 50, MinOrderAmount = 0, RequiredTier = null, IsExpired = true } }
    };

    public void Clear()
    {
        Items.Clear();
    }

    public void AddItem(string productName, int quantity, decimal unitPrice)
    {
        if (quantity <= 0)
            throw new ArgumentException("Số lượng phải lớn hơn 0");
        if (unitPrice < 0)
            throw new ArgumentException("Đơn giá không thể âm");

        Items.Add(new CartItem
        {
            ProductName = productName,
            Quantity = quantity,
            UnitPrice = unitPrice
        });
    }

    public decimal GetSubTotal()
    {
        return Items.Sum(i => i.TotalPrice);
    }

    public DiscountResult ApplyDiscount(string voucherCode, decimal? orderTotalOverride = null)
    {
        decimal orderTotal = orderTotalOverride ?? GetSubTotal();

        // 1. Kiểm tra voucher có tồn tại hoặc hết hạn không
        if (string.IsNullOrWhiteSpace(voucherCode) || !_vouchers.TryGetValue(voucherCode, out var voucher) || voucher.IsExpired)
        {
            return new DiscountResult
            {
                IsSuccess = false,
                DiscountAmount = 0,
                FinalAmount = orderTotal,
                ErrorMessage = "Mã giảm giá không hợp lệ hoặc đã hết hạn"
            };
        }

        // 2. Kiểm tra điều kiện hạng thành viên (nếu voucher yêu cầu)
        if (!string.IsNullOrEmpty(voucher.RequiredTier) && 
            !string.Equals(CustomerTier, voucher.RequiredTier, StringComparison.OrdinalIgnoreCase))
        {
            return new DiscountResult
            {
                IsSuccess = false,
                DiscountAmount = 0,
                FinalAmount = orderTotal,
                ErrorMessage = $"Mã này chỉ áp dụng cho hạng thành viên {voucher.RequiredTier}"
            };
        }

        // 3. Kiểm tra giá trị đơn hàng tối thiểu
        if (orderTotal < voucher.MinOrderAmount)
        {
            return new DiscountResult
            {
                IsSuccess = false,
                DiscountAmount = 0,
                FinalAmount = orderTotal,
                ErrorMessage = $"Đơn hàng chưa đạt mức tối thiểu {voucher.MinOrderAmount:N0} VND"
            };
        }

        // 4. Áp dụng giảm giá thành công
        decimal discount = Math.Round(orderTotal * (voucher.DiscountPercent / 100m), 2);
        decimal finalTotal = Math.Max(0, orderTotal - discount);

        return new DiscountResult
        {
            IsSuccess = true,
            DiscountAmount = discount,
            FinalAmount = finalTotal
        };
    }
}

