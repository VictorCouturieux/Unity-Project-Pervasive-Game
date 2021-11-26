
String unitySend;
 int i;

// RADIO
int radioRed = 5;
int radioBlue = 6;
int radioGreen = 7;

//INPUT A
int inputaRed = 8;
int inputaBlue = 9;
int inputaGreen = 10;

//INPUT B
int inputbRed = 11;
int inputbBlue =12;
int inputbGreen =13;

//INPUT C
int inputcRed = 14;
int inputcBlue = 15;
int inputcGreen = 16;

//DOOR
int doorRed = 17;
int doorGreen = 18;

void setup() {
 Serial.begin(9600);

// RADIO
  pinMode(radioRed, OUTPUT);
  pinMode(radioBlue, OUTPUT);   
  pinMode(radioGreen, OUTPUT);  

// INPUT A
  pinMode(inputaRed, OUTPUT);
  pinMode(inputaBlue, OUTPUT);   
  pinMode(inputaGreen, OUTPUT); 
  
// INPUT B
  pinMode(inputbRed, OUTPUT);
  pinMode(inputbBlue, OUTPUT);   
  pinMode(inputbGreen, OUTPUT); 

// INPUT C
  pinMode(inputcRed, OUTPUT);
  pinMode(inputcBlue, OUTPUT);   
  pinMode(inputcGreen, OUTPUT); 
  
// DOOR
  pinMode(doorRed, OUTPUT);
  pinMode(doorGreen, OUTPUT); 

  //*******************START COLOR****************
  
  digitalWrite(radioRed, LOW);
  digitalWrite(radioBlue, HIGH);
  digitalWrite(radioGreen, LOW);

  digitalWrite(inputaRed, LOW);
  digitalWrite(inputaBlue, LOW);
  digitalWrite(inputaGreen, LOW);

  digitalWrite(inputbRed, LOW);
  digitalWrite(inputbBlue, LOW);
  digitalWrite(inputbGreen, LOW);
  
  digitalWrite(inputcRed, LOW);
  digitalWrite(inputcBlue, LOW);
  digitalWrite(inputcGreen, LOW); 
 
 }

void loop() {

  if (Serial.available() > 0) {
  
  unitySend = Serial.readString();

  i = unitySend.toInt();
  Serial.println(i);

  switch (i) {
  case 5: //RADIO BLUE
      digitalWrite(radioRed, LOW);
      digitalWrite(radioBlue, HIGH);
      digitalWrite(radioGreen, LOW);    
    break;
  case 6: //RADIO GREEN
      digitalWrite(radioRed, LOW);
      digitalWrite(radioBlue, LOW);
      digitalWrite(radioGreen, HIGH);    
    break;
  case 7: //RADIO RED
      digitalWrite(radioRed, HIGH);
      digitalWrite(radioBlue, LOW);
      digitalWrite(radioGreen, LOW);   
    break;
  case 8: //RADIO YELLOW
       //ColorForRadio(0, 0, 255); 
      digitalWrite(radioRed, HIGH);
      digitalWrite(radioBlue, LOW);
      digitalWrite(radioGreen, HIGH);   
    break;      
  case 9: //RADIO BLACK
      digitalWrite(radioRed, LOW);
      digitalWrite(radioBlue, LOW);
      digitalWrite(radioGreen, LOW);    
    break; 
  case 10: //INPUT A BLUE
      digitalWrite(inputaRed, LOW);
      digitalWrite(inputaBlue, HIGH);
      digitalWrite(inputaGreen, LOW);    
    break; 
  case 11: //INPUT A YELLOW
      // ColorForInputA(0, 0, 255);
      digitalWrite(inputaRed, HIGH);
      digitalWrite(inputaBlue, LOW);
      digitalWrite(inputaGreen, HIGH);    
    break;
  case 12: //INPUT A GREEN
      digitalWrite(inputaRed, LOW);
      digitalWrite(inputaBlue, LOW);
      digitalWrite(inputaGreen, HIGH);    
    break;
  case 13: //INPUT A BLACK
      digitalWrite(inputaRed, LOW);
      digitalWrite(inputaBlue, LOW);
      digitalWrite(inputaGreen, LOW);    
    break;
  case 14: //INPUT B RED
      digitalWrite(inputbRed, HIGH);
      digitalWrite(inputbBlue, LOW);
      digitalWrite(inputbGreen, LOW);    
    break;
  case 15: //INPUT B YELLOW
       //ColorForInputB(0, 0, 255);
      digitalWrite(inputbRed, HIGH);
      digitalWrite(inputbBlue, LOW);
      digitalWrite(inputbGreen, HIGH);     
    break;
  case 16: //INPUT B GREEN
      digitalWrite(inputbRed, LOW);
      digitalWrite(inputbBlue, LOW);
      digitalWrite(inputbGreen, HIGH);    
    break;
  case 17: //INPUT B BLACK
      digitalWrite(inputbRed, LOW);
      digitalWrite(inputbBlue, LOW);
      digitalWrite(inputbGreen, LOW);    
    break;
  case 18: //INPUT C RED
      digitalWrite(inputcRed, HIGH);
      digitalWrite(inputcBlue, LOW);
      digitalWrite(inputcGreen, LOW);    
    break;
  case 19: //INPUT C YELLOW
      // ColorForInputC(0, 0, 255);
      digitalWrite(inputaRed, HIGH);
      digitalWrite(inputcBlue, LOW);
      digitalWrite(inputcGreen, HIGH);     
    break;    
  case 20: //INPUT C GREEN
      digitalWrite(inputcRed, LOW);
      digitalWrite(inputcBlue, LOW);
      digitalWrite(inputcGreen, HIGH);    
    break;
  case 21: //INPUT C BLACK
      digitalWrite(inputcRed, LOW);
      digitalWrite(inputcBlue, LOW);
      digitalWrite(inputcGreen, LOW);    
    break;
  case 22: //INPUT A RED
      digitalWrite(inputaRed, HIGH);
      digitalWrite(inputaBlue, LOW);
      digitalWrite(inputaGreen, LOW);        
    break; 

    
  case 23: //Reset light
    
    digitalWrite(radioRed, LOW);
    digitalWrite(radioBlue, LOW);
    digitalWrite(radioGreen, LOW);
  
    digitalWrite(inputaRed, LOW);
    digitalWrite(inputaBlue, LOW);
    digitalWrite(inputaGreen, LOW);
  
    digitalWrite(inputbRed, LOW);
    digitalWrite(inputbBlue, LOW);
    digitalWrite(inputbGreen, LOW);
    
    digitalWrite(inputcRed, LOW);
    digitalWrite(inputcBlue, LOW);
    digitalWrite(inputcGreen, LOW); 
    break;                    
  case 24: //Inputs to red
    digitalWrite(inputaRed, HIGH);
    digitalWrite(inputaBlue, LOW);
    digitalWrite(inputaGreen, LOW);
  
    digitalWrite(inputbRed, HIGH);
    digitalWrite(inputbBlue, LOW);
    digitalWrite(inputbGreen, LOW);
    
    digitalWrite(inputcRed, HIGH);
    digitalWrite(inputcBlue, LOW);
    digitalWrite(inputcGreen, LOW); 
    break;
  case 25: //Start scene 0
    digitalWrite(radioRed, LOW);
    digitalWrite(radioBlue, HIGH);
    digitalWrite(radioGreen, LOW);
  
    digitalWrite(inputaRed, LOW);
    digitalWrite(inputaBlue, LOW);
    digitalWrite(inputaGreen, LOW);
  
    digitalWrite(inputbRed, LOW);
    digitalWrite(inputbBlue, LOW);
    digitalWrite(inputbGreen, LOW);
    
    digitalWrite(inputcRed, LOW);
    digitalWrite(inputcBlue, LOW);
    digitalWrite(inputcGreen, LOW); 
    break;
  case 26: //Start scene 1
    digitalWrite(radioRed, LOW);
    digitalWrite(radioBlue, HIGH);
    digitalWrite(radioGreen, LOW);
  
    digitalWrite(inputaRed, LOW);
    digitalWrite(inputaBlue, LOW);
    digitalWrite(inputaGreen, LOW);
  
    digitalWrite(inputbRed, LOW);
    digitalWrite(inputbBlue, LOW);
    digitalWrite(inputbGreen, LOW);
    
    digitalWrite(inputcRed, LOW);
    digitalWrite(inputcBlue, LOW);
    digitalWrite(inputcGreen, LOW); 
    break;
  case 27: //Start scene 2
    digitalWrite(radioRed, LOW);
    digitalWrite(radioBlue, HIGH);
    digitalWrite(radioGreen, LOW);
  
    digitalWrite(inputaRed, LOW);
    digitalWrite(inputaBlue, LOW);
    digitalWrite(inputaGreen, LOW);
  
    digitalWrite(inputbRed, LOW);
    digitalWrite(inputbBlue, LOW);
    digitalWrite(inputbGreen, LOW);
    
    digitalWrite(inputcRed, LOW);
    digitalWrite(inputcBlue, LOW);
    digitalWrite(inputcGreen, LOW); 
    break;
  case 28: //Start scene 3
    digitalWrite(radioRed, LOW);
    digitalWrite(radioBlue, HIGH);
    digitalWrite(radioGreen, LOW);
  
    digitalWrite(inputaRed, LOW);
    digitalWrite(inputaBlue, HIGH);
    digitalWrite(inputaGreen, LOW);
  
    digitalWrite(inputbRed, LOW);
    digitalWrite(inputbBlue, LOW);
    digitalWrite(inputbGreen, LOW);
    
    digitalWrite(inputcRed, LOW);
    digitalWrite(inputcBlue, LOW);
    digitalWrite(inputcGreen, LOW); 
    break;
  case 29: //Start scene 4
    digitalWrite(radioRed, LOW);
    digitalWrite(radioBlue, HIGH);
    digitalWrite(radioGreen, LOW);
  
    digitalWrite(inputaRed, LOW);
    digitalWrite(inputaBlue, HIGH);
    digitalWrite(inputaGreen, LOW);
  
    digitalWrite(inputbRed, LOW);
    digitalWrite(inputbBlue, LOW);
    digitalWrite(inputbGreen, LOW);
    
    digitalWrite(inputcRed, LOW);
    digitalWrite(inputcBlue, LOW);
    digitalWrite(inputcGreen, LOW); 
    break;
  case 30: //Start scene 5
    digitalWrite(radioRed, LOW);
    digitalWrite(radioBlue, HIGH);
    digitalWrite(radioGreen, LOW);
  
    digitalWrite(inputaRed, LOW);
    digitalWrite(inputaBlue, HIGH);
    digitalWrite(inputaGreen, LOW);
  
    digitalWrite(inputbRed, LOW);
    digitalWrite(inputbBlue, LOW);
    digitalWrite(inputbGreen, LOW);
    
    digitalWrite(inputcRed, LOW);
    digitalWrite(inputcBlue, LOW);
    digitalWrite(inputcGreen, LOW);   
    break;
  case 31: //Start scene 6
    digitalWrite(radioRed, LOW);
    digitalWrite(radioBlue, HIGH);
    digitalWrite(radioGreen, LOW);
  
    digitalWrite(inputaRed, LOW);
    digitalWrite(inputaBlue, HIGH);
    digitalWrite(inputaGreen, LOW);
  
    digitalWrite(inputbRed, LOW);
    digitalWrite(inputbBlue, LOW);
    digitalWrite(inputbGreen, LOW);
    
    digitalWrite(inputcRed, LOW);
    digitalWrite(inputcBlue, LOW);
    digitalWrite(inputcGreen, LOW);  
    break;
  case 32: //Start scene 7
    digitalWrite(radioRed, LOW);
    digitalWrite(radioBlue, HIGH);
    digitalWrite(radioGreen, LOW);
  
    digitalWrite(inputaRed, LOW);
    digitalWrite(inputaBlue, LOW);
    digitalWrite(inputaGreen, HIGH);
  
    digitalWrite(inputbRed, LOW);
    digitalWrite(inputbBlue, LOW);
    digitalWrite(inputbGreen, HIGH);
    
    digitalWrite(inputcRed, LOW);
    digitalWrite(inputcBlue, LOW);
    digitalWrite(inputcGreen, HIGH);  
    break;
                
  default:
    break;
     }

  }


}
