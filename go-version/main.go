package main

import (
	"time"

	"github.com/gofiber/fiber/v2"
)

func main() {
	app := fiber.New()

	app.Get("/", func(c *fiber.Ctx) error {
		return c.JSON(fiber.Map{
			"message": "Hello, World!",
		})
	})

	// return a joke as an json object
	app.Get("/joke", func(c *fiber.Ctx) error {
		return c.JSON(fiber.Map{
			"joke": "I'm not a regular developer, I'm a cool developer.",
		})
	})

	// return the current time as a json object
	app.Get("/time", func(c *fiber.Ctx) error {
		return c.JSON(fiber.Map{
			"time": time.Now(),
		})
	})

	app.Listen(":3000")
}
