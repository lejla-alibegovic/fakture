## Opis zadatka
Potrebno je razviti sustav koji će omogućiti korisnicima stvaranje i pregledavanje faktura. Faktura se izdaje
nakon što se proda neki proizvod ili usluga.

Faktura obavezno sadržava sljedeće informacije:
- broj fakture,
- datum stvaranja fakture,
- datum dospijeća fakture (tj. do kad fakturu treba platiti),
- kolekciju stavki:
  - opis prodane stavke,
  - količina prodane stavke,
  - jedinična cijena stavke bez poreza,
  - ukupna cijena za stavku bez poreza (broj stavki * jedinična cijena bez poreza),
- ukupna cijena bez poreza (zbroj ukupnih cijena svih stavki),
- ukupna cijena s porezom,
- stvaratelja računa (korisnik iz sustava),
- naziv primatelja računa (samo string, može biti i prazan).

Prilikom stvaranja računa je potrebno odabrati koji će se porez primjenjivati (npr. hrvatski PDV od 25%, BiH
PDV od 17%, ili nešto slično…). Komponente koje računaju porez se moraju dodati kao ekstenzije i dinamički
se učitati. Prilikom stvaranja računa je potrebno izabrati kakvo se računanje poreza primjenjuje.
Sustav mora imati i korisnike koji se moraju registrirati i logirati u sustav kako bi mogli stvarati i pregledavati
račune.

## Tehnički zahtjevi
- Sustav je potrebno implementirati koristeći ASP.NET MVC5.
- Za komunikaciju s bazom se treba koristiti Entity Framerowk 6 i Code First pristup.
- Korisnike je potrebno implementirati koristeći Identity Framework.
- Za rad s ekstenzijama je potrebno koristiti MEF
