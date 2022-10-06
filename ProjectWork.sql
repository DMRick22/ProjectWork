CREATE DATABASE ProjectWork;

USE ProjectWork;

CREATE TABLE Utenti
(
id INT PRIMARY KEY IDENTITY(1,1),
nome VARCHAR(100),
cognome VARCHAR(100),
dob DATE,
username VARCHAR(100),
psw  VARCHAR(100),
ruolo  VARCHAR(100),
oreTotali int
);


INSERT INTO Utenti
(nome,cognome,dob,username,psw,ruolo,oreTotali)
VALUES
('Alice','Bensanelli','1997-05-12','Alice',HASHBYTES('Sha2_512','Alice123'),'Amministratore',0),
('Mattia','Bertaglia','1997-07-01','Mattia',HASHBYTES('Sha2_512','Mattia123'),'Amministratore',0),
('Diego','Sforza','1980-04-03','Diego',HASHBYTES('Sha2_512','D123'),'Utente',0),
('Sara','Metta','1999-04-22','Sara',HASHBYTES('Sha2_512','S123'),'Utente',0),
('Mara','Bianchi','2000-06-12','Mara',HASHBYTES('Sha2_512','M123'),'Utente',0),
('Luca','Lancioni','1980-05-12','Luca',HASHBYTES('Sha2_512','L123'),'Utente',0),
('Matteo','Rossi','1977-01-29','Matteo',HASHBYTES('Sha2_512','M123'),'Utente',0),
('Giovanni','Allegretti','1981-03-09','Giovanni',HASHBYTES('Sha2_512','G123'),'Utente',0);

CREATE TABLE Corsi 
(
    id int PRIMARY KEY IDENTITY(1,1),
    copertina VARCHAR(300),
    nome VARCHAR(200),
    autore VARCHAR(100),
    linguaggioTrattato VARCHAR(100),
    durata INT,
    numeroLezioni INT,
    costo FLOAT,
    categoria VARCHAR(100),
    descrizione VARCHAR(400)
);


INSERT INTO Corsi
(copertina,nome,autore,linguaggioTrattato,durata,numeroLezioni,costo,categoria,descrizione)
VALUES
('https://www.myti.it/wp-content/uploads/2018/12/PHP_Logo.png','PHP','devacademy','PHP',200,40,500.99,'back-end','PHP Hypertext Preprocessor è un potente linguaggio di scripting che si lascia incorporare facilmente all''interno del codice HTML ed offre un potente strumento per realizzare pagine dinamiche con collegamenti a database relazionali come MySql.'),
('https://www.aktsrl.com/wp-content/uploads/2022/05/img-articolo-java-2-1080x675.jpg','JAVA','elearning','Java',200,40,500.99,'back-end','Nel corso verrà trattata la progettazione software, la programmazione strutturata, la creazione di interfacce grafiche, l''incapsulamento, l''ereditarietà, il polimorfismo, ecc. Si svilupperanno esercizi pratici al fine di poter apprendere correttamente le procedure dello sviluppo di un''applicazione Java.'),
('https://prod-discovery.edx-cdn.org/media/course/image/b5f62d16-ac55-4b5f-ab39-d19dc635d158-ad1d78dfab3a.small.jpeg','PYTON','mrw','Pyton',100,25,300,'back-end','Python è un linguaggio di programmazione dinamico orientato agli oggetti utilizzabile per molti tipi di sviluppo software. Offre un forte supporto all''integrazione con altri linguaggi e programmi, è fornito di una estesa libreria standard e può essere imparato in pochi giorni.'),
('https://www.tutorialrepublic.com/lib/images/html-illustration.png','HTML5','develop4fun','Html',60,10,100,'front-end','L’HTML fa parte di quelle conoscenze informatiche che rendono solide le basi di qualunque professionista digitale, anche di quelli che si dedicano alla sicurezza delle piattaforme o alla creazione di strutture per l’archiviazione e la condivisione in rete dei dati.'),
('https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQMs_ErQtC5QWG1XC0Fs63Uwydcm9-NgVCS9g&usqp=CAU','CSS3','kinsta','Css',60,10,205.99,'front-end','Corso CSS completo ed approfondito sui fogli di stile, aggiornato alla versione CSS 3, tratta la stilizzazione delle pagine Web partendo dal presupposto che il discente abbia già delle conoscenze almeno sufficienti di HTML.'),
('https://developmania.altervista.org/wp-content/uploads/2019/09/javascript.png','JAVASCRIPT','icubed','Javascript',90,15,110,'front-end','Il Corso JavaScript Multilevel è rivolto è rivolto a coloro che desiderano apprendere questo linguaggio, fornendo una capacità di analisi dei problemi e le conoscenze necessarie sulla sintassi del linguaggio Javascript in modo che fin dalle prime lezioni acquisiranno la capacità di scrivere le prime righe di codice.'),
('https://cdn.ucberkeleybootcamp.com/wp-content/uploads/sites/106/2020/03/SQL-Coding-Class-San-Francisco-1.jpeg','SQL','Microsoft','Sql',220,36,330.90,'dbms','Il corso si prefigge di introdurre i partecipanti al Linguaggio SQL, utilizzato per interrogare e modificare i database partendo dagli statements più semplici fino ad arrivare alla scrittura di script elaborati.'),
('https://i1.wp.com/www.kallo.it/wp-content/uploads/2016/07/mysql-logo.jpg?fit=1020%2C426&ssl=1','MySQL','Oracle','MySQL',200,25,250,'dbms','MySQL è un RDBMS open source e libero, e rappresenta una delle tecnologie più note e diffuse nel mondo dell’IT. L’obiettivo di questo corso è quello di fornire allo studente le conoscenze base per sfruttare le potenzialità di MySQL.'),
('https://miro.medium.com/max/800/1*XIMVb4ZQRfSS4ZnI6WfH0Q.jpeg','PostgreSQL','Gianni Ciolli','PostgreSQL',150,30,350.85,'dbms','L’obiettivo principale del corso PostrgreSQL per sviluppatori è fornire ai partecipanti le competenze teoriche e tecniche per progettare un database efficiente, sviluppare applicazioni di qualità e utilizzare comandi SQL avanzati.'),
('https://i0.wp.com/www.fabiobernini.it/wp-content/uploads/2019/04/sqlserver.png?fit=400%2C400&ssl=1','Mssql','Fabio Bernini','Mssql',120,30,199.99,'dbms','Il Corso su SQL Server ha come obiettivo la formazione di una figura in grado di installare, configurare, amministrare, gestire e manipolare database relazionali client-server Microsoft SQL Server tramite lo strumento Query Analyzer.');


CREATE TABLE Prenotazioni
(
    id int PRIMARY KEY IDENTITY(1,1),
    dataPrenotazione date,
    idUtenti INT,
    idCorsi INT,
    FOREIGN KEY (idUtenti) REFERENCES Utenti(id) ON UPDATE CASCADE ON DELETE SET NULL,
    FOREIGN KEY (idCorsi) REFERENCES Corsi(id) ON UPDATE CASCADE ON DELETE SET NULL
);

INSERT INTO Prenotazioni
(dataPrenotazione,idUtenti,idCorsi)
VALUES
('2022-08-10',5,4),
('2022-09-15',3,6),
('2022-10-23',4,1),
('2022-11-22',7,9),
('2022-01-15',6,7);


SELECT * FROM Utenti;

SELECT * FROM Corsi;

SELECT * FROM Prenotazioni;



