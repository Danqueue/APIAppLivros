create table tb_livros
(
Id int auto_increment primary key,
Titulo varchar(255),
Autor varchar(150),
Genero varchar(150),
AnoPublicacao int,
NumeroPaginas int
);

select * from tb_livros
