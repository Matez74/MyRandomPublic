#ifndef LCD_DISPLAY_H
#define LCD_DISPLAY_H
#include <Arduino.h>
#include <Wire.h>

#define LCD_CLEARDISPLAY   0x01
#define LCD_RETURNHOME    0x02
#define LCD_ENTRYMODESET  0x04
#define LCD_DISPLAYCONTROL 0x08
#define LCD_FUNCTIONSET   0x20
#define LCD_SETDDRAMADDR  0x80

#define LCD_ENTRYLEFT     0x02
#define LCD_2LINE         0x08
#define LCD_4BITMODE      0x00
#define LCD_5x8DOTS       0x00
#define LCD_DISPLAYON     0x04
#define LCD_CURSOROFF    0x00
#define LCD_BLINKOFF     0x00

#define En 0b00000100
#define Rs 0b00000001
#define LCD_BACKLIGHT 0x08

class LcdDisplay {
public:
    LcdDisplay(uint8_t addr = 0x27, uint8_t cols = 16, uint8_t rows = 2);

    void init();
    void clear();
    void setCursor(uint8_t col, uint8_t row);
    void print(const char* text);
    void backlight(bool on = true);

private:
    void command(uint8_t value);
    void send(uint8_t value, uint8_t mode);
    void write4bits(uint8_t value);
    void pulseEnable(uint8_t data);
    void expanderWrite(uint8_t data);

    uint8_t _addr;
    uint8_t _cols;
    uint8_t _rows;
    uint8_t _backlight;
};

#endif