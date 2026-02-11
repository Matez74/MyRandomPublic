#include "LcdDisplay.h"

LcdDisplay::LcdDisplay(uint8_t addr, uint8_t cols, uint8_t rows)
    : _addr(addr), _cols(cols), _rows(rows), _backlight(LCD_BACKLIGHT) {}

void LcdDisplay::init() {
    Wire.begin();
    delay(100);

    expanderWrite(_backlight);
    delay(1000);

    write4bits(0x03 << 4);
    delayMicroseconds(4500);
    write4bits(0x03 << 4);
    delayMicroseconds(4500);
    write4bits(0x03 << 4);
    delayMicroseconds(150);
    write4bits(0x02 << 4);

    command(LCD_FUNCTIONSET | LCD_4BITMODE | LCD_2LINE | LCD_5x8DOTS);
    command(LCD_DISPLAYCONTROL | LCD_DISPLAYON | LCD_CURSOROFF | LCD_BLINKOFF);
    command(LCD_ENTRYMODESET | LCD_ENTRYLEFT);

    clear();
}

void LcdDisplay::clear() {
    command(LCD_CLEARDISPLAY);
    delayMicroseconds(2000);
}

void LcdDisplay::setCursor(uint8_t col, uint8_t row) {
    static const uint8_t row_offsets[] = {0x00, 0x40, 0x14, 0x54};
    if (row >= _rows) row = _rows - 1;
    command(LCD_SETDDRAMADDR | (col + row_offsets[row]));
}

void LcdDisplay::print(const char* text) {
    while (*text) {
        send(*text++, Rs);
    }
}

void LcdDisplay::backlight(bool on) {
    _backlight = on ? LCD_BACKLIGHT : 0;
    expanderWrite(0);
}


void LcdDisplay::command(uint8_t value) {
    send(value, 0);
}

void LcdDisplay::send(uint8_t value, uint8_t mode) {
    write4bits((value & 0xF0) | mode);
    write4bits(((value << 4) & 0xF0) | mode);
}

void LcdDisplay::write4bits(uint8_t value) {
    expanderWrite(value);
    pulseEnable(value);
}

void LcdDisplay::expanderWrite(uint8_t data) {
    Wire.beginTransmission(_addr);
    Wire.write(data | _backlight);
    Wire.endTransmission();
}

void LcdDisplay::pulseEnable(uint8_t data) {
    expanderWrite(data | En);
    delayMicroseconds(1);
    expanderWrite(data & ~En);
    delayMicroseconds(50);
}