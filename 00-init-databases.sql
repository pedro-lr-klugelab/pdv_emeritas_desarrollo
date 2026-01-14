-- Script de inicialización de bases de datos
-- Este script crea las bases de datos necesarias antes de importar los datos

CREATE DATABASE IF NOT EXISTS `farmacontrol_global` CHARACTER SET utf8 COLLATE utf8_general_ci;
CREATE DATABASE IF NOT EXISTS `farmacontrol_local` CHARACTER SET utf8 COLLATE utf8_general_ci;

-- Asegurar que el usuario existe y otorgar permisos
GRANT ALL PRIVILEGES ON `farmacontrol_global`.* TO 'farmacontrol'@'%' IDENTIFIED BY 'farmacontrol123';
GRANT ALL PRIVILEGES ON `farmacontrol_local`.* TO 'farmacontrol'@'%' IDENTIFIED BY 'farmacontrol123';
GRANT ALL PRIVILEGES ON `farmacontrol_global`.* TO 'farmacontrol'@'localhost' IDENTIFIED BY 'farmacontrol123';
GRANT ALL PRIVILEGES ON `farmacontrol_local`.* TO 'farmacontrol'@'localhost' IDENTIFIED BY 'farmacontrol123';
FLUSH PRIVILEGES;

-- Seleccionar base de datos para el siguiente script
USE `farmacontrol_global`;
