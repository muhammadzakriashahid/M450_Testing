# Welche Elemente braucht es für eine Teststrategie?
### Teststrategie und Planung
- Strategie: beschreibt vorgehen beim Testen.
- Testkonzept: wichtigste Test-Elemente werden festgelegt.
- 4 wichtige punkte:
    - Auflisten was genau getestet wird.
    - Testmethode auswählen (z.B. Black-Box oder White-Box)
    - Testinfrastruktur festlegen (resources used for testing)
    - Testobjekte mit einer kurzen Auflistung aus welchen Teilen die Software besteht (z.B. Module, Klassen, Funktionen)
### Testobjekte
- hier handelt es sich um Applikationen Teilen/Komponenten die getestet werden. Modules oder Units (z.B. eine Klasse)
 aber auch scripts und prozesse.
### Testfälle -> "Herzstück" des Testkonzepts
- was wird genau getestet?
- dabei wird zwischen funktionale und nicht-funktionale Testfällen unterschieden.
    - Funktionale-Testfälle: hier wird vor allem das Verhalten der Software getestet, also ob die Anforderungen 
    erfüllt sind. das "was" die Software tut, wird getestet
    - Nicht-Funktionale-Testfälle: die haben generell nicht zutun mit der Applikation, z.B. Performance, Sicherheit.
    das "wie" die Software etwas tut, wird getestet.
- Abstrakte Testfälle: da gibts es keine konkrete Inputs/Outputs, logische Operatoren wie ">" oder "<" werden verwendet.
- Konkrete Testfälle: hier werden konkrete Inputs/Outputs verwendet. Die werden oft aus der Anforderungen abgeleitet.
### Test-Methoden auswählen
- hier schaut man auf "wie" wird getestet? sind es Black-Box oder White-Box Tests? sind es manuelle oder 
automatisierte Tests?
    - White-Box Testfälle (Code-Pfad Testing): hier werden Unit-Tests ausgeführt, also der Code ist sichtbar und kann $
    getestet werden. 
    - Black-Box Testfälle: hier kennt man den code nicht, bzw. es ist schon kompiliert und deployed, und somit nicht 
    sichtbar. das wird von eine Testteam durchgeführt und dokumentiert.
    - Automatisierung von Testfälle: hier werden die Testfälle automatisiert ausgeführt.