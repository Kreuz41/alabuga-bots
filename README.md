### Подготовка
Устанавливаем гит с оффициального сайта: https://git-scm.com/downloads
Устанавливаем докер с оффициального сайта: https://www.docker.com/products/docker-desktop/

### Запуск
Клонируем репозиторий командой
```
git clone https://github.com/Kreuz41/alabuga-bots.git
```

Затем переходим в директиву запуска
```
cd alabuga-bots/Build
```

Затем необходимо создать два файла ```bot1.env```  и ```bot2.env``` в папке Build. Необходимо заполнить файлы согласно примеру в файле ```.env.sample```
Важно! Нужно заполнить поля согласно тому, какой это бот. Для ботов должны быть разные токены. ChatId - идентификатор чата в который бот будет пересылать сообщения. Если это чат или канал, то перед id дожен быть -100. Наприер конфигурация может выглядеть так:
```
BotToken="123123:qweeqweqweewq"
ChatId="-100123123123"
```

После всех манипуляций необходимо запустить бота командой
```
docker compose up --build
```
