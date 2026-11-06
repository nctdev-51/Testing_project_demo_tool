@bdd
@cart
Feature: Áp dụng mã giảm giá và tính tiền giỏ hàng
  Để kích thích khách hàng mua sắm
  Là một hệ thống thương mại điện tử
  Tôi muốn tự động tính toán tổng tiền và áp dụng ưu đãi hợp lệ

  Background: Khởi tạo giỏ hàng ban đầu
    Given Khách hàng là thành viên "VIP"
    And Giỏ hàng hiện đang trống

  @smoke
  Scenario: Thêm nhiều mặt hàng vào giỏ và kiểm tra tạm tính
    When Khách hàng thêm danh sách sản phẩm sau vào giỏ:
      | TenSanPham  | SoLuong | DonGia |
      | Chuot Gaming| 2       | 500000 |
      | Ban Phim Co | 1       | 1000000|
    Then Tổng tiền tạm tính phải là 2000000 VND

  @regression
  Scenario Outline: Kiểm tra giảm giá theo hạng thành viên và giá trị đơn hàng
    Given Khách hàng có hạng thành viên là "<HangThanhVien>"
    And Đơn hàng có tổng trị giá là <TongTien> VND
    When Khách hàng áp dụng mã giảm giá "<MaVoucher>"
    Then Số tiền được giảm phải là <SoTienGiam> VND
    And Số tiền thực trả là <ThucTra> VND
    And Kết quả áp dụng phải là "<ThanhCong>"

    Examples:
      | HangThanhVien | TongTien | MaVoucher | SoTienGiam | ThucTra  | ThanhCong |
      | Standard      | 500000   | SALE10    | 50000      | 450000   | true      |
      | VIP           | 1000000  | VIP20     | 200000     | 800000   | true      |
      | Silver        | 200000   | SALE10    | 0          | 200000   | false     |

  @boundary
  Scenario Outline: Kiểm tra giá trị biên của mức đơn tối thiểu SALE10
    Given Khách hàng có hạng thành viên là "Standard"
    And Đơn hàng có tổng trị giá là <TongTien> VND
    When Khách hàng áp dụng mã giảm giá "SALE10"
    Then Số tiền được giảm phải là <SoTienGiam> VND
    And Số tiền thực trả là <ThucTra> VND
    And Kết quả áp dụng phải là "<ThanhCong>"

    Examples:
      | TongTien | SoTienGiam | ThucTra | ThanhCong |
      | 499999   | 0          | 499999  | false     |
      | 500000   | 50000      | 450000  | true      |

  @negative
  Scenario: Xử lý khi mã giảm giá hết hạn hoặc không hợp lệ
    Given Đơn hàng có tổng trị giá là 500000 VND
    When Khách hàng áp dụng mã giảm giá "EXPIRED99" đã hết hạn
    Then Hệ thống phải báo lỗi "Mã giảm giá không hợp lệ hoặc đã hết hạn"
    And Số tiền thực trả giữ nguyên là 500000 VND

  @negative
  Scenario: Từ chối voucher VIP khi khách hàng không thuộc hạng VIP
    Given Khách hàng có hạng thành viên là "Standard"
    And Đơn hàng có tổng trị giá là 1000000 VND
    When Khách hàng áp dụng mã giảm giá "VIP20"
    Then Hệ thống phải báo lỗi "Mã này chỉ áp dụng cho hạng thành viên VIP"
    And Số tiền được giảm phải là 0 VND
    And Số tiền thực trả giữ nguyên là 1000000 VND
    And Kết quả áp dụng phải là "false"
