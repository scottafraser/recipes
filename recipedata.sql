-- phpMyAdmin SQL Dump
-- version 4.7.7
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Jul 19, 2018 at 11:50 PM
-- Server version: 5.6.38
-- PHP Version: 7.2.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `RecipeBox`
--
CREATE DATABASE IF NOT EXISTS `RecipeBox` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `RecipeBox`;

-- --------------------------------------------------------

--
-- Table structure for table `methods`
--

CREATE TABLE `methods` (
  `id` int(11) NOT NULL,
  `step` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `methods`
--

INSERT INTO `methods` (`id`, `step`) VALUES
(28, 'bake it'),
(29, 'dfdasf'),
(30, 'Toast it'),
(31, 'eat it');

-- --------------------------------------------------------

--
-- Table structure for table `methods_recipes`
--

CREATE TABLE `methods_recipes` (
  `id` int(11) NOT NULL,
  `method_id` int(11) NOT NULL,
  `recipe_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `methods_recipes`
--

INSERT INTO `methods_recipes` (`id`, `method_id`, `recipe_id`) VALUES
(28, 28, 35),
(29, 29, 36),
(30, 30, 37),
(31, 31, 37);

-- --------------------------------------------------------

--
-- Table structure for table `recipes`
--

CREATE TABLE `recipes` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `rating` int(11) NOT NULL,
  `ingredients` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `recipes`
--

INSERT INTO `recipes` (`id`, `name`, `rating`, `ingredients`) VALUES
(35, 'pizza', 5, 'bread, cheese, tomato'),
(36, 'cheese', 5, 'cheese, cheese'),
(37, 'Grilled Cheese', 0, 'bread, butter, cheese');

-- --------------------------------------------------------

--
-- Table structure for table `recipes_tags`
--

CREATE TABLE `recipes_tags` (
  `id` int(11) NOT NULL,
  `recipe_id` int(11) NOT NULL,
  `tag_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `recipes_tags`
--

INSERT INTO `recipes_tags` (`id`, `recipe_id`, `tag_id`) VALUES
(4, 37, 5);

-- --------------------------------------------------------

--
-- Table structure for table `tags`
--

CREATE TABLE `tags` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `tags`
--

INSERT INTO `tags` (`id`, `name`) VALUES
(1, '#breakfast'),
(2, '#poorfood'),
(3, '#hearty'),
(4, '#breakfast'),
(5, '#awesome');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `methods`
--
ALTER TABLE `methods`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `methods_recipes`
--
ALTER TABLE `methods_recipes`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `recipes`
--
ALTER TABLE `recipes`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `recipes_tags`
--
ALTER TABLE `recipes_tags`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `tags`
--
ALTER TABLE `tags`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `methods`
--
ALTER TABLE `methods`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=32;

--
-- AUTO_INCREMENT for table `methods_recipes`
--
ALTER TABLE `methods_recipes`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=32;

--
-- AUTO_INCREMENT for table `recipes`
--
ALTER TABLE `recipes`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=38;

--
-- AUTO_INCREMENT for table `recipes_tags`
--
ALTER TABLE `recipes_tags`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `tags`
--
ALTER TABLE `tags`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
