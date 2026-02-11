#include "Web.h"
#include "SetUpAndMemory.h"

SetUpAndMemory memo;

unsigned long lastMil = 0;

Web::Web(const char* ssid, const char* pass)
    : ssid(ssid), pass(pass), server(80)
{
}

void Web::init() {
    Serial.begin(9600);
    while (!Serial);

    Serial.println("Inicializace WiFi");

    WiFi.begin(ssid, pass);
    while (WiFi.status() != WL_CONNECTED) {
        delay(500);
        Serial.print(".");
    }

    Serial.println("\nWiFi připojeno");
    Serial.println(WiFi.localIP());

    memo.init();
    server.begin();
}

void Web::handle() {

    if (millis() - lastMil >= 2000) {
        lastMil = millis();
        memo.updateValues();
    }

    WiFiClient client = server.available();
    if (!client) return;

    String request = "";
    while (client.connected()) {
        if (client.available()) {
            char c = client.read();
            request += c;
            if (request.endsWith("\r\n\r\n")) break;
        }
    }

    handleRequest(request);
    sendPage(client);

    client.stop();
}

void Web::handleRequest(String request) {

    if (request.indexOf("GET /servo?pos=0") >= 0)
     memo.setServoAngle(0);

    if (request.indexOf("GET /servo?pos=90") >= 0)
     memo.setServoAngle(90);

    if (request.indexOf("GET /servo?pos=180") >= 0)
     memo.setServoAngle(180);

    if (request.indexOf("GET /servo?pos=") >= 0) {
        int start = request.indexOf("pos=") + 4;
        int end = request.indexOf(" ", start);
        String angleStr = request.substring(start, end);
         int angle = angleStr.toInt();

        Serial.println(angle);

        if (angle >= 0 && angle <= 180) {
        memo.setServoAngle(angle);
        }
}


    if (request.indexOf("GET /oled?text=") >= 0) {
        int start = request.indexOf("text=") + 5;
        int end = request.indexOf(" ", start);
        String text = request.substring(start, end);

        text.replace("%20", " ");
        text.replace("+", " ");

        Serial.print("LCD text: ");
        Serial.println(text);

        memo.writeTextLCD(text.c_str());
    }
}

void Web::sendPage(WiFiClient& client) {

    client.println("HTTP/1.1 200 OK");
    client.println("Content-Type: text/html; charset=utf-8");
    client.println("Connection: close");
    client.println();

    client.println("<!DOCTYPE HTML>");
    client.println("<html>");
    client.println("<head>");
    client.println("<meta charset=\"UTF-8\">");
    client.println("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">");
    client.println("<title>Řízení Arduino projektu</title>");
    client.println("</head>");

    client.println("<body>");
    client.println("<h1>Řízení Arduino projektu</h1>");

    client.println("<h2>Senzory</h2>");
    client.print("<p>Teplota: ");
    client.print(memo.getTemperature());
    client.println(" °C</p>");

    client.print("<p>Vlhkost: ");
    client.print(memo.getHumidity());
    client.println(" %</p>");

    client.print("<p>Vzdálenost: ");
    client.print(memo.getDistance());
    client.println(" cm</p>");

    client.println("<h2>Servo motor</h2>");
    client.println("<a href=\"/servo?pos=0\"><button>0°</button></a>");
    client.println("<a href=\"/servo?pos=90\"><button>90°</button></a>");
    client.println("<a href=\"/servo?pos=180\"><button>180°</button></a>");

    client.println("<form action=\"/servo\" method=\"GET\">");
    client.println("Úhel (0–180): <input type=\"number\" name=\"pos\" min=\"0\" max=\"180\">");
    client.println("<input type=\"submit\" value=\"Nastavit\">");
    client.println("</form>");

    client.println("<h2>LCD displej</h2>");
    client.println("<form action=\"/oled\" method=\"GET\">");
    client.println("Text: <input type=\"text\" name=\"text\" maxlength=\"20\">");
    client.println("<input type=\"submit\" value=\"Zobrazit\">");
    client.println("</form>");

    client.println("</body>");
    client.println("</html>");
}
