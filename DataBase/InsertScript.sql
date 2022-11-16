INSERT INTO public."Utilizador"(
	"Username", "Password", "Admin", "Estado")
	VALUES 
		('jfigueiras', 'abc', true, true),
		('naraujo', 'zxc', false, true),
		('vmachado', 'qwe', false, true),
		('tazevedo', 'vbn', false, false);

INSERT INTO public."Job"(
	"Nome")
	VALUES 
		('Job1'),
		('Job2'),
		('Job3'),
		('Job4'),
		('Job5');

INSERT INTO public."Maquina"(
	"Nome")
	VALUES 
		('Maquina1'),
		('Maquina2'),
		('Maquina3'),
		('Maquina4'),
		('Maquina5');

INSERT INTO public."Operacao"(
	"Nome")
	VALUES 
		('Operacao1'),
		('Operacao2'),
		('Operacao3'),
		('Operacao4'),
		('Operacao5');

INSERT INTO public."Simulacao"(
	"Nome")
	VALUES 
		('Simulacao1'),
		('Simulacao2'),
		('Simulacao3'),
		('Simulacao4'),
		('Simulacao5');

INSERT INTO public."Plano"(
	"IdUtilizador", "IdSimulacao", "IdJob", "IdOperacao", "IdMaquina", "TempoInicial", "UnidadeTempo", "TempoFinal", "Estado", "PosOperacao")
	VALUES 
		(1, 1, 1, 1, 3, 0, 1, 0, true, 1),
		(1, 1, 1, 2, 1, 0, 5, 0, true, 2),
		(1, 1, 1, 3, 2, 0, 3, 0, true, 3),
		(1, 1, 1, 4, 3, 0, 5, 0, true, 4),
		(1, 1, 2, 1, 1, 0, 2, 0, true, 1),
		(1, 1, 2, 2, 4, 0, 4, 0, true, 2),
		(1, 1, 2, 3, 3, 0, 2, 0, true, 3),
		(1, 1, 2, 4, 2, 0, 2, 0, true, 4),
		(1, 1, 3, 1, 2, 0, 2, 0, true, 1),
		(1, 1, 3, 2, 4, 0, 2, 0, true, 2),
		(1, 1, 3, 3, 1, 0, 5, 0, true, 3),
		(1, 1, 3, 4, 5, 0, 1, 0, true, 4);