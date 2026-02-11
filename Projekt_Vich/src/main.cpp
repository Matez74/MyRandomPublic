#include <Arduino.h>
#include "Web.h"
Web web("Arduino", "arduinoH2");

void setup() {
    web.init();
}

void loop() {
    web.handle();
}