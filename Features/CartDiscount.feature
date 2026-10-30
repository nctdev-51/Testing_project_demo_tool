Feature: Áp dụng mã giảm giá và tính tiền giỏ hàng
  Để kích thích khách hàng mua sắm
  Là một hệ thống thương mại điện tử
  Tôi muốn tự động tính toán tổng tiền và áp dụng ưu đãi hợp lệ

  Background: Khởi tạo giỏ hàng ban đầu
    Given Khách hàng là thành viên "VIP"
    And Giỏ hàng hiện đang trống

  Scenario: Thêm nhiều mặt hàng vào giỏ và kiểm tra tạm tính
    When Khách hàng thêm danh sách sản phẩm sau vào giỏ:
      | TenSanPham  | SoLuong | DonGia |
      | Chuot Gaming| 2       | 500000 |
      | Ban Phim Co | 1       | 1000000|
    Then Tổng tiền tạm tính phải là 2000000 VND

  Scenario Outline: Kiểm tra giảm giá theo hạng thành viên và giá trị đơn hàng
    Given Khách hàng có hạng thành viên là "<HangThanhVien>"
    And Đơn hàng có tổng trị giá là <TongTien> VND
    When Khách hàng áp dụng mã giảm giá "<MaVoucher>"
    Then Số tiền được giảm phải là <SoTienGiam> VND
    And Số tiền thực trả là <ThucTra> VND

    Examples:
      | HangThanhVien | TongTien | MaVoucher | SoTienGiam | ThucTra  |
      | Standard      | 500000   | SALE10    | 50000      | 450000   |
      | VIP           | 1000000  | VIP20     | 200000     | 800000   |
      | Silver        | 200000   | SALE10    | 0          | 200000   |

  Scenario: Xử lý khi mã giảm giá hết hạn hoặc không hợp lệ
    Given Đơn hàng có tổng trị giá là 500000 VND
    When Khách hàng áp dụng mã giảm giá "EXPIRED99" đã hết hạn
    Then Hệ thống phải báo lỗi "Mã giảm giá không hợp lệ hoặc đã hết hạn"
    And Số tiền thực trả giữ nguyên là 500000 VND