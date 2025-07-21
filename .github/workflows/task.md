# You are a senior C# .NET backend developer
# Tracking status 
[]-[~]-[!]-[*] each will be todo-in progress - done -  blocked respectively
- Tracking and coming back here to update status of each task when task changes
# Note:
![db diagram](image.png)
# This is your task
[!] Khi add new delivery thì delivery detail không được trống (khi đó sẽ ktra product exist và  QuantityAvailable trong inventories có đủ không rồi trừ đi QuantityAvailable trong inventories) 
[!] khi update delivery mà delivery status là 4 (cancelled) thì tự động lấy các số lượng quantity của các deliverydetail mà có delivery id là trùng cộng lại cho QuantityAvailable bảng inventories
[!] không cho xoá delivery, dùng api update đổi status là 4
[!] khi add một delivery detail vào trong delivery thì sẽ ktra product exist và  QuantityAvailable trong inventories có đủ không rồi trừ đi QuantityAvailable trong inventories
[!] khi update một delivery detail mà update số lượng lớn hơn số ban đầu thì ktra số thay đổi của delivery detail có còn đủ không trong rồi trừ đi QuantityAvailable trong inventories (ví dụ thay đổi từ 2 sang 3 thì xem còn đủ 1 trong inventories không)
[!] khi update một delivery detail mà update số lượng nhỏ hơn số ban đầu thì cộng vào QuantityAvailable trong inventories (ví dụ từ 2 sang 1 )
[!] khi xoá một delivery detail thì cộng hết số lượng vào lại QuantityAvailable trong inventories 
[!] nếu mà delivery status là 2 (delivered và shipped) thì không cho update các delivery detail và cả delivery là customer id
[] Khi mà tạo mới delivery detail thì cần kiểm tra xem delivery id có status là pending thì mới cho tạo và ExpectedDate là tương lai.
[!] Khi update delivery detail thì cần  kiểm tra xem delivery id có status là pending thì mới cho update 
[!] Và đối với các tác vụ thêm, sửa, xóa của delivery và delivery detail bạn có thể thêm transaction roll back cho tôi được không
[] Nếu khi tạo mới một  delivery detail mà product id đó đã có sẵn trong delivery đó rồi thì thay vì tạo mới hãy update deliveryQuantity của delivery detail có product ID trong delivery đó


# Warning:
- If you dont understand or concern at something, please raise question before doing 
- Before execute, please ask me make sure to do it