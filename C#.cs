const int redPin = 3;
const int greenPin = 5;
const int bluePin = 6;

void setup() {
  
  Serial.begin(9600);
  
  pinMode(5, OUTPUT);
  pinMode(4, OUTPUT);
  pinMode(3, OUTPUT);
}

void loop() {
  
  while (Serial.available() > 0) {

    
    int red = Serial.parseInt();
    int green = Serial.parseInt();
    int blue = Serial.parseInt();

    
    if (Serial.read() == '\n') {
      
      red = 255 - constrain(red, 0, 255);
      green = 255 - constrain(green, 0, 255);
      blue = 255 - constrain(blue, 0, 255);


      analogWrite(5, red);
      analogWrite(4, green);
      analogWrite(3, blue);

      
      Serial.print(red, HEX);
      Serial.print(green, HEX);
      Serial.println(blue, HEX);
    }
  }
}

