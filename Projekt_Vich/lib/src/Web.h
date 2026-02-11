#ifndef WEB_H
#define WEB_H

#include <Arduino.h>
#include <WiFiS3.h>
#include <WiFiServer.h>

class Web {
public:
    Web(const char* ssid, const char* pass);
    void init();
    void handle();

private:
    const char* ssid;
    const char* pass;

    WiFiServer server;

    void handleRequest(String request);
    void sendPage(WiFiClient& client);
};

#endif