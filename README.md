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



Đã cấu hình AddSignalR() và MapHub<PingHub>("/pingHub").



Kết quả chạy server:



!\[SignalR Server](anh/buoc2-server.png)



Server chạy thành công tại http://localhost:5282.

