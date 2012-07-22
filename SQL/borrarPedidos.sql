delete from tiempo_estado;
delete from linea_pedido;
delete from pedido;
delete from plan_produccion;
delete from orden_de_trabajo;

ALTER SEQUENCE plan_produccion_idplan_seq MINVALUE 0 START 1;
ALTER SEQUENCE orden_de_trabajo_idorden_seq MINVALUE 0 START 1;
ALTER SEQUENCE pedido_idpedido_seq MINVALUE 0 START 1;
