-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Aug 22, 2023 at 01:56 AM
-- Server version: 10.4.28-MariaDB
-- PHP Version: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `lms`
--

-- --------------------------------------------------------

--
-- Table structure for table `book`
--

CREATE TABLE `book` (
  `BID` int(11) NOT NULL,
  `BName` varchar(50) NOT NULL,
  `BAuthor` varchar(50) NOT NULL,
  `BPublisher` varchar(50) NOT NULL,
  `BPrice` float NOT NULL,
  `BQTY` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `issue`
--

CREATE TABLE `issue` (
  `INO` int(11) NOT NULL,
  `MID` int(11) NOT NULL,
  `MName` varchar(50) NOT NULL,
  `BID` int(11) NOT NULL,
  `BName` varchar(50) NOT NULL,
  `issue_Date` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `librian`
--

CREATE TABLE `librian` (
  `LID` int(11) NOT NULL,
  `LName` varchar(50) NOT NULL,
  `Lphone` int(10) NOT NULL,
  `Lpass` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `librians`
--

CREATE TABLE `librians` (
  `LID` int(11) NOT NULL,
  `LName` varchar(50) NOT NULL,
  `Lphone` int(10) NOT NULL,
  `Lpass` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `members`
--

CREATE TABLE `members` (
  `MID` int(11) NOT NULL,
  `MName` varchar(50) NOT NULL,
  `MAddress` varchar(200) NOT NULL,
  `MPhone` int(10) NOT NULL,
  `MAge` int(3) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `returntb`
--

CREATE TABLE `returntb` (
  `RNO` int(11) NOT NULL,
  `MID` int(11) NOT NULL,
  `MName` varchar(50) NOT NULL,
  `MAddress` varchar(200) NOT NULL,
  `BID` int(11) NOT NULL,
  `BName` varchar(50) NOT NULL,
  `issue_Date` date NOT NULL,
  `return_Date` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `book`
--
ALTER TABLE `book`
  ADD PRIMARY KEY (`BID`);

--
-- Indexes for table `issue`
--
ALTER TABLE `issue`
  ADD PRIMARY KEY (`INO`);

--
-- Indexes for table `librian`
--
ALTER TABLE `librian`
  ADD PRIMARY KEY (`LID`);

--
-- Indexes for table `librians`
--
ALTER TABLE `librians`
  ADD PRIMARY KEY (`LID`);

--
-- Indexes for table `members`
--
ALTER TABLE `members`
  ADD PRIMARY KEY (`MID`);

--
-- Indexes for table `returntb`
--
ALTER TABLE `returntb`
  ADD PRIMARY KEY (`RNO`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `book`
--
ALTER TABLE `book`
  MODIFY `BID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=126;

--
-- AUTO_INCREMENT for table `issue`
--
ALTER TABLE `issue`
  MODIFY `INO` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `librian`
--
ALTER TABLE `librian`
  MODIFY `LID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT for table `librians`
--
ALTER TABLE `librians`
  MODIFY `LID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=203;

--
-- AUTO_INCREMENT for table `members`
--
ALTER TABLE `members`
  MODIFY `MID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `returntb`
--
ALTER TABLE `returntb`
  MODIFY `RNO` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=400;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
