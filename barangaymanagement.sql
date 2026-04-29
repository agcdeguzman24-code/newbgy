-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 29, 2026 at 06:29 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `barangaymanagement`
--

-- --------------------------------------------------------

--
-- Table structure for table `announcement`
--

CREATE TABLE `announcement` (
  `id` int(11) NOT NULL,
  `Title` varchar(100) NOT NULL,
  `Author` varchar(100) NOT NULL,
  `DatePosted` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `announcement`
--

INSERT INTO `announcement` (`id`, `Title`, `Author`, `DatePosted`) VALUES
(1, 'Feast', 'Kapitan', '2026-04-27');

-- --------------------------------------------------------

--
-- Table structure for table `blotter`
--

CREATE TABLE `blotter` (
  `id` int(11) NOT NULL,
  `Blotter ID` varchar(100) NOT NULL,
  `Date` date NOT NULL,
  `Compliant` varchar(500) NOT NULL,
  `IncidentType` varchar(150) NOT NULL,
  `Location` varchar(100) NOT NULL,
  `Status` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `blotter`
--

INSERT INTO `blotter` (`id`, `Blotter ID`, `Date`, `Compliant`, `IncidentType`, `Location`, `Status`) VALUES
(1, 'Reklamo', '2026-04-27', 'Shabu user', 'Got hit by addict', 'Balanga City Bataan', 'Pending');

-- --------------------------------------------------------

--
-- Table structure for table `certificate`
--

CREATE TABLE `certificate` (
  `id` int(11) NOT NULL,
  `RequestID` varchar(50) NOT NULL,
  `RequestorName` varchar(50) NOT NULL,
  `DocumentType` varchar(50) NOT NULL,
  `status` varchar(20) NOT NULL,
  `daterequested` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `certificate`
--

INSERT INTO `certificate` (`id`, `RequestID`, `RequestorName`, `DocumentType`, `status`, `daterequested`) VALUES
(1, '23', 'Oggy', 'BarangayClearance', 'Accepted', '2026-04-27'),
(2, '44', 'Andrew', 'BarangayClearance', 'Accepted', '2026-04-27'),
(3, '55', 'Seab', 'BarangayClearance', 'Accepted', '2026-04-27'),
(4, '60', 'Sean', 'Certificate of Indigency', 'Accepted', '2026-04-27'),
(5, '5', 'Moana', 'Barangay Clearance', 'Rejected', '2026-04-27');

-- --------------------------------------------------------

--
-- Table structure for table `residents`
--

CREATE TABLE `residents` (
  `id` int(11) NOT NULL,
  `fname` varchar(50) NOT NULL,
  `lname` varchar(50) NOT NULL,
  `bday` date NOT NULL,
  `gender` varchar(10) NOT NULL,
  `address` varchar(100) NOT NULL,
  `contactnumber` varchar(15) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `residents`
--

INSERT INTO `residents` (`id`, `fname`, `lname`, `bday`, `gender`, `address`, `contactnumber`) VALUES
(1, 'Alexis', 'Estrada', '2026-04-15', 'Male', 'Pilar Bataan', '09640436346'),
(2, 'andrew', 'bunja', '2026-04-02', 'Male', 'Mariveles Bataan', ''),
(3, 'Zeus', 'Thunder', '2026-04-30', 'Male', 'Bagac Bataan', '09640436346');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `email` varchar(100) NOT NULL,
  `password` varchar(100) NOT NULL,
  `role` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`id`, `email`, `password`, `role`) VALUES
(1, 'bpsu@gmail.com', 'Staff1234', 'staff'),
(2, 'kapitan21@gmail.com', 'Kapitan221', 'Staff');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `announcement`
--
ALTER TABLE `announcement`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `blotter`
--
ALTER TABLE `blotter`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `certificate`
--
ALTER TABLE `certificate`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `residents`
--
ALTER TABLE `residents`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `announcement`
--
ALTER TABLE `announcement`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `blotter`
--
ALTER TABLE `blotter`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `certificate`
--
ALTER TABLE `certificate`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `residents`
--
ALTER TABLE `residents`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
