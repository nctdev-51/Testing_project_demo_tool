using System;

namespace BasicExercises.Exercises;

/// <summary>
/// Lớp minh họa ứng dụng console thực tế sử dụng CartService để quản lý giỏ hàng và thanh toán.
/// Nhóm có thể gọi hàm CartConsoleDemo.Run() hoặc trình bày cho giảng viên xem cách component vận hành trong ứng dụng thực.
/// </summary>
public static class CartConsoleDemo
{
    public static void Run()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("=================================================");
        Console.WriteLine("       HỆ THỐNG QUẢN LÝ BÁN HÀNG & GIỎ HÀNG      ");
        Console.WriteLine("=================================================");

        var cart = new CartService { CustomerTier = "VIP" };
        Console.WriteLine($"[+] Khởi tạo khách hàng: Hạng {cart.CustomerTier}");

        // Thêm sản phẩm
        Console.WriteLine("\n[+] Đang thêm sản phẩm vào giỏ hàng...");
        cart.AddItem("Chuột Gaming Logitech", 2, 500000);
        cart.AddItem("Bàn phím cơ DareU", 1, 1000000);

        Console.WriteLine("--- CHI TIẾT GIỎ HÀNG ---");
        foreach (var item in cart.Items)
        {
            Console.WriteLine($"- {item.ProductName} | SL: {item.Quantity} | Đơn giá: {item.UnitPrice:N0} VND | Thành tiền: {item.TotalPrice:N0} VND");
        }

        decimal subTotal = cart.GetSubTotal();
        Console.WriteLine($"=> Tổng tiền tạm tính: {subTotal:N0} VND");

        // Áp dụng voucher hợp lệ
        Console.WriteLine("\n[+] Áp dụng mã giảm giá 'VIP20'...");
        var result = cart.ApplyDiscount("VIP20");
        if (result.IsSuccess)
        {
            Console.WriteLine($"[THÀNH CÔNG] Giảm giá: -{result.DiscountAmount:N0} VND");
            Console.WriteLine($"=> SỐ TIỀN THỰC TRẢ: {result.FinalAmount:N0} VND");
        }
        else
        {
            Console.WriteLine($"[THẤT BẠI] Lỗi: {result.ErrorMessage}");
        }

        // Thử áp dụng voucher hết hạn
        Console.WriteLine("\n[+] Thử áp dụng mã hết hạn 'EXPIRED99'...");
        var expiredResult = cart.ApplyDiscount("EXPIRED99");
        if (!expiredResult.IsSuccess)
        {
            Console.WriteLine($"[BÁO LỖI] {expiredResult.ErrorMessage}");
            Console.WriteLine($"=> Giữ nguyên số tiền thanh toán: {expiredResult.FinalAmount:N0} VND");
        }

        Console.WriteLine("\n=================================================");
        Console.WriteLine("             HOÀN TẤT PHIÊN GIAO DỊCH            ");
        Console.WriteLine("=================================================");
    }
}

