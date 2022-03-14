# Tisch-Arcade

## Voraussetzungen

Sogenannte „shebang“ an den Anfang der Script-Datei hinzufügen (bestimmen mit welchem Programm das Spiel auszuführen ist)
z.B. für Python:		#!/usr/bin/env python3
Die Auflösung des Arcade ist 1280x1024. Das Spiel muss die gleiche Auflösung haben und/oder im Vollbildmodus starten.
Der Name der im Menü Angezeigt wird, ist der Name des Ordners, in dem die Datei ist. 
Nur ein Programm pro Ordner ist erlaubt. Wenn aus irgendwelchen Gründen mehrere Ausführbare Dateien in einem Ordner sind kann man ein „_“ an den Anfang des Namens schreiben und das Menü überspringt dieses. (z.B. _nichtausführen.py)
Beim Beenden des Programms muss sichergestellt werden, dass ggf. alle Threads, Fenster, und Sounds richtig geschlossen werden. (z.B. für PyGame: pygame.quit())
Das Programm muss aus dem Spiel heraus schließbar sein.

## Speicherort
Damit das Menü die Datei findet, muss diese in /home/arcade/games/DEINSPIEL/SPIELDATEI.EXT Platziert werden. Bitte stell sicher, dass nur eine ausführbare Datei in diesem Ordner ist. Wenn eure Datei nicht als Ausführbare Datei gesehen wird, Informiert bitte den Administrator, derzeit wird nur eine Begrenzte Anzahl an Endungen erkannt.
Das Menü zeigt Game-Icons an. Es sucht diese im gleichen Ordner, wie die auszuführende Datei. Die Bilddatei muss dafür eine .ico Endung haben.
Das Menü kann zudem ein Screenshot des Spiels anzeigen, wenn dieses ausgewählt ist. Die Bilddatei kann dafür .png oder .jpeg sein.
