
 ## Установка
 - Cклонируйте репозиторий: git clone https://github.com/SuhrobJumaev/NotificationService.git
 - Перейдите в директорию проекта: cd NotificationService
 - В дириектории NotificationService: выполнить команду docker-compose up --build
 - Подождать пока проект забилдится и запуститься в контейнере.
 - **Важно:** При первом запуске, после закачки всех образов докера, нужно подождать 1 или 2 минуты все будет зависит от скорости интернета, пока запуститься контейнер с БД. После запуска БД, приложения автоматически создаст нужную таблицу.
 - Приложения будет доступно по адресу: http://localhost:8080/swagger/index.html
 - HealthCheck - http://localhost:8080/_health