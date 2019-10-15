# PSMove-Midi-Drum
PSMove MIDI Drum, es una aplicaci´on que utiliza dispositivos PSMove para simular la ejecución de una batería de tambores
y percusión (drum). La aplicación es desarrollada utilizando el servicio PSMoveService https://github.com/psmoveservice/PSMoveService, la GUI es desarrollada con el motor Unity, de la cual es aprovechada sus funcionalidades para la detección de colisiones, las cuales generan eventos (triggers) que son transformados a su vez, dependiendo del instrumento
virtual colisionado, en eventos MIDI. Para la generación de los eventos MIDI se utiliza la biblioteca NAudio https://github.com/naudio/NAudio.

https://youtu.be/PeMeFtDK3Sw
