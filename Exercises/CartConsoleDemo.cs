using System;
using CartApp.Core;

namespace BasicExercises.Exercises;

/// <summary>
/// Ứng dụng Console chuyên nghiệp minh họa cách CartService vận hành trong thực tế và cách kiểm thử tự động (Assertions).
/// Nhóm có thể gọi CartConsoleDemo.Run() để trình chiếu bảng điều khiển ASCII màu sắc trực tiếp cho giảng viên.
/// </summary>
public static class CartConsoleDemo
{
    public static void Run()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        
        // Header Banner
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(@"╔════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine(@"║         E-COMMERCE CART & DISCOUNT ENGINE – CONSOLE DEMO           ║");
        Console.WriteLine(@"║         (Minh họa BDD Testing: CartDiscount.feature)               ║");
        Console.WriteLine(@"╚════════════════════════════════════════════════════════════════════╝");
        Console.ResetColor();

        // 1. Given: Khởi tạo giỏ hàng cho khách VIP
        var cart = new CartService { CustomerTier = "VIP" };
        Console.WriteLine("\n[1] BDD GIVEN: Khách hàng là thành viên [VIP]");

        // 2. When: Thêm sản phẩm
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n[2] BDD WHEN: Thêm danh sách sản phẩm vào giỏ hàng:");
        Console.ResetColor();
        cart.AddItem("Chuột Gaming Logitech", 2, 500000);
        cart.AddItem("Bàn phím cơ DareU RGB", 1, 1000000);

        Console.WriteLine("    ┌───────────────────────────┬──────────┬──────────────┬──────────────┐");
        Console.WriteLine("    │ Tên sản phẩm              │ Số lượng │ Đơn giá      │ Thành tiền   │");
        Console.WriteLine("    ├───────────────────────────┼──────────┼──────────────┼──────────────┤");
        foreach (var item in cart.Items)
        {
            Console.WriteLine($"    │ {item.ProductName,-25} │ {item.Quantity,8} │ {item.UnitPrice,10:N0} ₫ │ {item.TotalPrice,10:N0} ₫ │");
        }
        Console.WriteLine("    └───────────────────────────┴──────────┴──────────────┴──────────────┘");

        decimal subTotal = cart.GetSubTotal();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"    => Tổng tiền tạm tính (SubTotal): {subTotal:N0} ₫");
        Console.ResetColor();

        // 3. When: Áp dụng voucher VIP20
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n[3] BDD WHEN: Áp dụng mã giảm giá 'VIP20' (Giảm 20% cho đơn từ 1.000.000 ₫)...");
        Console.ResetColor();

        var result = cart.ApplyDiscount("VIP20");
        if (result.IsSuccess)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"    [✔ THÀNH CÔNG] Chiết khấu: -{result.DiscountAmount:N0} ₫ (20%)");
            Console.WriteLine($"    => SỐ TIỀN THỰC TRẢ (FinalAmount): {result.FinalAmount:N0} ₫");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"    [✖ THẤT BẠI] Lỗi: {result.ErrorMessage}");
            Console.ResetColor();
        }

        // 4. Then: Chạy bộ Assertions tự động
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\n[4] BDD THEN: KIỂM CHỨNG BỘ ASSERTIONS TỰ ĐỘNG:");
        Console.ResetColor();

        CheckAssertion("SubTotal == 2,000,000 ₫", subTotal == 2000000);
        CheckAssertion("DiscountAmount == 400,000 ₫ (20% của 2tr)", result.DiscountAmount == 400000);
        CheckAssertion("FinalAmount == 1,600,000 ₫", result.FinalAmount == 1600000);

        // 5. Thử nghiệm mã hết hạn
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n[5] KIỂM THỬ NGOẠI LỆ: Thử áp dụng voucher đã hết hạn 'EXPIRED99'...");
        Console.ResetColor();

        var expiredResult = cart.ApplyDiscount("EXPIRED99");
        bool isHandledCorrectly = !expiredResult.IsSuccess && expiredResult.FinalAmount == subTotal;
        CheckAssertion("Chặn mã hết hạn & Giữ nguyên giá trị thanh toán", isHandledCorrectly);

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n======================================================================");
        Console.WriteLine("    KẾT QUẢ: TOÀN BỘ CÁC BƯỚC KIỂM THỬ ĐÃ HOÀN TẤT VÀ VƯỢT QUA ✔     ");
        Console.WriteLine("======================================================================\n");
        Console.ResetColor();
    }

    private static void CheckAssertion(string title, bool condition)
    {
        if (condition)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"    [✔ PASSED] {title}");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"    [✖ FAILED] {title}");
        }
        Console.ResetColor();
    }
}
