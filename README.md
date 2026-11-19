\# Bài 3 - SignalR



\## Bước 1: Tạo solution và build project



Đã tạo solution PingSignalR gồm hai project:



\- Ping.Api: ASP.NET Core Web.

\- Ping.Client: Console Client sử dụng SignalR.



Đã cài thư viện Microsoft.AspNetCore.SignalR.Client phiên bản 10.0.15.



Kết quả build:



!\[Kết quả build](anh/buoc1-build.png)



Build succeeded, không có lỗi.



\## Bước 2: Xây dựng SignalR Hub



Đã tạo PingHub kế thừa lớp Hub của SignalR. Hub hỗ trợ phương thức Echo để nhận tin nhắn và gửi phản hồi về client gọi.



Đã cấu hình AddSignalR() và MapHub<PingHub>("/hubs/ping").



Kết quả chạy server:



!\[SignalR Server](anh/buoc2-server.png)



Server chạy thành công tại http://localhost:5282.



\## Bước 3: Kết nối Client với SignalR Server



Đã cấu hình Ping.Client kết nối đến SignalR Hub thông qua địa chỉ http://localhost:5282/hubs/ping.



Client gọi phương thức Echo với nội dung "Xin chao SignalR" và nhận phản hồi từ Server.



Kết quả chạy thử:



!\[Kết quả Echo](anh/buoc3-echo.png)



Client kết nối và nhận phản hồi thành công.



\## Bước 4: Kiểm tra tự động kết nối lại



Đã cấu hình WithAutomaticReconnect() và bổ sung các sự kiện Reconnecting, Reconnected, Closed để theo dõi trạng thái kết nối.



Tiến hành tắt Server rồi khởi động lại. Client tự động phát hiện mất kết nối và kết nối lại thành công.



Kết quả kiểm thử:



!\[Automatic Reconnect](anh/buoc4-noilai.png)



Client hiển thị thông báo "Dang ket noi lai..." và "Da ket noi lai".



\## Bước 5: Kiểm tra Negotiate



Sử dụng lệnh curl để gửi yêu cầu đến endpoint negotiate của SignalR Hub.



Kết quả kiểm tra:



!\[Kết quả Negotiate](anh/buoc5-negotiate.png)



\*\*Kết quả:\*\* Server trả về HTTP 200 OK, kèm connectionId, connectionToken và danh sách phương thức truyền tải.



\*\*Giải thích:\*\* Negotiate giúp Client và Server trao đổi thông tin trước khi thiết lập kết nối SignalR.

