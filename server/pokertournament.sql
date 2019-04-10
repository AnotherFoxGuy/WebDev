-- phpMyAdmin SQL Dump
-- version 4.8.5
-- https://www.phpmyadmin.net/
--
-- Host: localhost
-- Generation Time: Apr 10, 2019 at 12:34 PM
-- Server version: 8.0.15
-- PHP Version: 7.2.15-0ubuntu0.18.04.2

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `pokertournament`
--

-- --------------------------------------------------------

--
-- Table structure for table `Game`
--

CREATE TABLE `Game` (
  `Game_ID` int(11) NOT NULL,
  `Game_Finished` tinyint(4) NOT NULL DEFAULT '0',
  `Time_Left` time DEFAULT NULL,
  `Current_Round` int(11) NOT NULL,
  `Pokertables` int(11) NOT NULL,
  `Gameprofile_Profile_ID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `Gameprofile`
--

CREATE TABLE `Gameprofile` (
  `Profile_ID` int(11) NOT NULL,
  `Profilename` varchar(45) NOT NULL,
  `Pause_Time` time NOT NULL,
  `Pause_After` int(11) NOT NULL,
  `Starting_Budget` int(11) NOT NULL,
  `Rebuy` int(11) NOT NULL,
  `Rules` text,
  `Chips` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `Game_has_Round`
--

CREATE TABLE `Game_has_Round` (
  `Game_Game_ID` int(11) NOT NULL,
  `Round_Round_ID` int(11) NOT NULL,
  `Round_NR` int(11) DEFAULT NULL,
  `Round_Time` time DEFAULT NULL,
  `Small_Blind` int(11) DEFAULT NULL,
  `Big_Blind` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `Poker_Table`
--

CREATE TABLE `Poker_Table` (
  `Table_ID` int(11) NOT NULL,
  `Table_Name` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Current_Game_ID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `Round`
--

CREATE TABLE `Round` (
  `Round_ID` int(11) NOT NULL,
  `Round_Time` time NOT NULL,
  `Small_Blind` int(11) NOT NULL,
  `Big_Blind` int(11) NOT NULL,
  `Gameprofile_Profile_ID` int(11) NOT NULL,
  `Round_NR` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `User`
--

CREATE TABLE `User` (
  `User_ID` int(11) NOT NULL,
  `Firstname` varchar(45) NOT NULL,
  `Lastname` varchar(45) NOT NULL,
  `Wins` int(11) NOT NULL DEFAULT '0',
  `Join_Game` tinyint(4) NOT NULL DEFAULT '0',
  `Poker_Table_Table_ID` int(11) DEFAULT NULL,
  `Avatar` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `Game`
--
ALTER TABLE `Game`
  ADD PRIMARY KEY (`Game_ID`),
  ADD UNIQUE KEY `Game_ID_UNIQUE` (`Game_ID`),
  ADD KEY `fk_Current_Gameprofile1_idx` (`Gameprofile_Profile_ID`);

--
-- Indexes for table `Gameprofile`
--
ALTER TABLE `Gameprofile`
  ADD PRIMARY KEY (`Profile_ID`),
  ADD UNIQUE KEY `Profile_ID_UNIQUE` (`Profile_ID`);

--
-- Indexes for table `Game_has_Round`
--
ALTER TABLE `Game_has_Round`
  ADD PRIMARY KEY (`Game_Game_ID`,`Round_Round_ID`),
  ADD KEY `fk_Game_has_Round_Round1_idx` (`Round_Round_ID`),
  ADD KEY `fk_Game_has_Round_Game1_idx` (`Game_Game_ID`);

--
-- Indexes for table `Poker_Table`
--
ALTER TABLE `Poker_Table`
  ADD PRIMARY KEY (`Table_ID`),
  ADD UNIQUE KEY `Table_ID_UNIQUE` (`Table_ID`),
  ADD KEY `fk_Poker_Table_Current1_idx` (`Current_Game_ID`);

--
-- Indexes for table `Round`
--
ALTER TABLE `Round`
  ADD PRIMARY KEY (`Round_ID`),
  ADD UNIQUE KEY `Round_NR_UNIQUE` (`Round_ID`),
  ADD KEY `fk_Round_Gameprofile_idx` (`Gameprofile_Profile_ID`);

--
-- Indexes for table `User`
--
ALTER TABLE `User`
  ADD PRIMARY KEY (`User_ID`),
  ADD UNIQUE KEY `User_ID_UNIQUE` (`User_ID`),
  ADD KEY `fk_User_Poker_Table1_idx` (`Poker_Table_Table_ID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `Game`
--
ALTER TABLE `Game`
  MODIFY `Game_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=153;

--
-- AUTO_INCREMENT for table `Gameprofile`
--
ALTER TABLE `Gameprofile`
  MODIFY `Profile_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=44;

--
-- AUTO_INCREMENT for table `Poker_Table`
--
ALTER TABLE `Poker_Table`
  MODIFY `Table_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=221;

--
-- AUTO_INCREMENT for table `Round`
--
ALTER TABLE `Round`
  MODIFY `Round_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1576;

--
-- AUTO_INCREMENT for table `User`
--
ALTER TABLE `User`
  MODIFY `User_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=45;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `Game`
--
ALTER TABLE `Game`
  ADD CONSTRAINT `fk_Current_Gameprofile1` FOREIGN KEY (`Gameprofile_Profile_ID`) REFERENCES `Gameprofile` (`Profile_ID`);

--
-- Constraints for table `Game_has_Round`
--
ALTER TABLE `Game_has_Round`
  ADD CONSTRAINT `fk_Game_has_Round_Game1` FOREIGN KEY (`Game_Game_ID`) REFERENCES `Game` (`Game_ID`),
  ADD CONSTRAINT `fk_Game_has_Round_Round1` FOREIGN KEY (`Round_Round_ID`) REFERENCES `Round` (`Round_ID`);

--
-- Constraints for table `Poker_Table`
--
ALTER TABLE `Poker_Table`
  ADD CONSTRAINT `fk_Poker_Table_Current1` FOREIGN KEY (`Current_Game_ID`) REFERENCES `Game` (`Game_ID`);

--
-- Constraints for table `Round`
--
ALTER TABLE `Round`
  ADD CONSTRAINT `fk_Round_Gameprofile` FOREIGN KEY (`Gameprofile_Profile_ID`) REFERENCES `Gameprofile` (`Profile_ID`);

--
-- Constraints for table `User`
--
ALTER TABLE `User`
  ADD CONSTRAINT `fk_User_Poker_Table1` FOREIGN KEY (`Poker_Table_Table_ID`) REFERENCES `Poker_Table` (`Table_ID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
