# BasicExercises
I. Quy trình chuẩn cho mọi thành viên khi làm Phần A
1.	Pull project mẫu về máy: Lấy bộ khung BasicExercises (đã có sẵn cấu hình .csproj, class Support/CsvTable.cs và Bài 1 mẫu của Chí Tâm) từ GitHub về. 
2.	Tạo bộ 4 file cho bài tập được giao: Mỗi thành viên tự hoàn thiện trọn vẹn 1 bài theo khung code → feature → CSV → steps tại đúng các thư mục Exercises/, Features/, Data/ và StepDefinitions/. 
3.	Đảm bảo đủ 3 lớp kiểm thử:
o	Viết Examples inline (3–5 dòng mẫu ngay trong file .feature) để người xem đọc hiểu ngay. 
o	Tạo file dữ liệu .csv (9–13 dòng gồm các giá trị biên và phân vùng tương đương) gắn với kịch bản qua tag @DataSource. 
o	Đảm bảo bộ dữ liệu phủ hết các nhánh điều kiện để phục vụ bước đo độ phủ mã (Coverage) bằng coverlet. 
4.	3 lưu ý kỹ thuật bắt buộc để không bị lỗi mất Test trên Visual Studio:
o	Xóa dòng chặn trong .csproj: Ngay sau khi tạo file .feature mới, mở file BasicExercises.csproj kiểm tra; nếu thấy dòng <ReqnrollFeatureFile Remove="..."/> tự sinh ra thì xóa ngay dòng đó đi rồi lưu lại. 
o	Cú pháp Tag: Mỗi tag (@blackbox, @csv, @DataSource:...) phải đặt trên một dòng riêng biệt, viết liền và không để dư khoảng trắng.
o	Đường dẫn CSV: Do file .feature nằm trong thư mục con Features/, đường dẫn ở tag bắt buộc phải thêm ../ ở đầu (ví dụ: @DataSource:../Data/grade.csv). 
5.	Kiểm tra và chuẩn bị kịch bản "ăn tiền" khi Demo: Nhấn Ctrl + Shift + B để Build, mở Test Explorer (chọn chế độ Group by Traits) để chạy xanh toàn bộ test, sau đó chuẩn bị sẵn thao tác cài lỗi (bug) theo phân công để trình diễn lúc thuyết trình. 
II. Công việc cụ thể của từng thành viên
2. Ngô Tấn Tài 
•	Phần A (Bài 2 - Xếp loại điểm):
o	Tự tạo và hoàn thiện 4 file: Exercises/GradeCalculator.cs, Features/Grade.feature (gồm cả Examples inline và CSV), Data/grade.csv và StepDefinitions/GradeSteps.cs. 
o	Xây dựng bộ dữ liệu grade.csv (12 dòng từ -0.1 đến 10.1) kiểm tra sát từng giá trị biên và hai phía của biên theo thang điểm giả định. 
o	Chuẩn bị thao tác demo (nhánh cài lỗi): sửa điều kiện >= 8.5 thành > 8.5 trong code để chứng minh dòng biên 8.5,A trong CSV phát hiện lỗi và chuyển đỏ ngay. 
•	Phần B (ShopDemo): Phụ trách viết kịch bản Cart.feature và kiểm thử hộp đen cho chức năng giỏ hàng. 
•	Báo cáo Word cần nộp:
o	Bổ sung vào Mục 2.2 ưu điểm: "Kiểm thử hướng dữ liệu (Data-Driven) với nguồn CSV bên ngoài". 
o	Viết thuyết minh Bài 2 (phân tích phân vùng tương đương và giá trị biên) và phần kiểm thử hộp đen Cart.feature kèm hình chụp màn hình Test Explorer (lúc chạy xanh và lúc cài lỗi báo đỏ). 
3. Phan Văn Tâm 
•	Phần A (Bài 3 - Ngày hợp lệ, năm nhuận & Báo cáo Coverage):
o	Tự tạo và hoàn thiện 4 file: Exercises/DateUtil.cs, Features/Date.feature, Data/date.csv (12 dòng dữ liệu) và StepDefinitions/DateSteps.cs (lưu ý xử lý ký tự / hoặc đổi định dạng ngày sang dấu - để không bị lỗi Cucumber Expressions). 
o	Sau khi cả nhóm nộp đủ 5 bài Phần A, chạy lệnh đo độ phủ hộp trắng (dotnet test với XPlat Code Coverage và reportgenerator) để xuất ra thư mục báo cáo HTML (coverage/index.html) cho cả 5 class. 
o	Chuẩn bị thao tác demo: mở trực tiếp báo cáo HTML coverage/index.html trên trình duyệt, chỉ vào DateUtil.cs phủ xanh toàn bộ và giải thích bảng quyết định năm nhuận. 
•	Phần B (ShopDemo): Phụ trách phần kiểm thử hộp trắng giỏ hàng (Cart.WhiteBox). 
•	Báo cáo Word cần nộp:
o	Bổ sung hướng dẫn cài đặt Reqnroll.ExternalData và công cụ đo coverage vào Mục 3. 
o	Vẽ Bảng quyết định (Decision Table) của quy tắc năm nhuận (chia hết cho 4/100/400) và chỉ rõ từng cột tương ứng với 4 dòng năm nhuận (2024, 2023, 1900, 2000) trong date.csv. 
o	Chụp ảnh báo cáo Coverage HTML của 5 bài tập kèm nhận xét (chứng minh thiết kế test hộp đen EP + BVA tốt sẽ đạt độ phủ mã hộp trắng cao) và viết thuyết minh cho phần Cart.WhiteBox. 
4. Lương Việt Thái 
•	Phần A (Bài 4 - Kiểm tra mật khẩu):
o	Tự tạo và hoàn thiện 4 file: Exercises/PasswordValidator.cs, Features/Password.feature, Data/password.csv (8 dòng kiểm tra các quy tắc) và StepDefinitions/PasswordSteps.cs. 
o	Kiểm tra xử lý dòng thứ 7 trong CSV (mật khẩu rỗng); nếu plugin bỏ qua hoặc báo lỗi dòng rỗng thì tách trường hợp mật khẩu rỗng ra một Scenario riêng viết tay trong Password.feature. 
o	Chuẩn bị thao tác demo: trình diễn dòng cuối (abc) vi phạm 3 luật cùng lúc và trả về chuỗi kết hợp TooShort;NoUpper;NoDigit. 
•	Phần B (ShopDemo): Phụ trách xây dựng kịch bản và code kiểm thử API và hộp xám (API $\leftrightarrow$ Core). 
•	Báo cáo Word cần nộp:
o	Sửa lại cách đánh số các tiểu mục trong Mục 4 (Hướng dẫn sử dụng) từ "Bước 1, Bước 2..." sang định dạng số chuẩn 4.1., 4.2., 4.2.1. theo đúng yêu cầu trình bày báo cáo. 
o	Viết thuyết minh Bài 4 (giải thích việc kết hợp nhiều điều kiện độc lập và vì sao dòng abc mang tính chất kiểm thử hộp xám nhẹ) cùng phần thuyết minh + hình chụp kết quả chạy test API hộp xám. 
5. Lưu Chí Tài 
•	Phần A (Bài 5 - Tính tiền điện bậc thang & So sánh Cách A/B):
o	Tự tạo và hoàn thiện 4 file: Exercises/ElectricBill.cs, Features/Electric.feature (gồm kịch bản đọc CSV và kịch bản kiểm tra số âm -5 kWh ném ngoại lệ), Data/electric.csv (9 dòng tại các biên bậc thang 50/51, 100/101, 200/201) và StepDefinitions/ElectricSteps.cs. 
o	Chuẩn bị thao tác demo: sửa sai kỳ vọng 1 dòng trong electric.csv để trình chiếu thông báo lỗi trên Test Explorer; đồng thời chạy đối chiếu Cách A (@DataSource) và Cách B (@csv-manual gom toàn bộ dòng CSV vào 1 test). 
•	Phần B (ShopDemo): Phụ trách phần kiểm thử giao diện UI Selenium áp dụng mô hình Page Object, đồng thời quay trước 1 video chạy Selenium để dự phòng khi lên lớp demo. 

