<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Platformer Game</title>
  <style>
    canvas {
      display: block;
      margin: 0 auto;
      background: #87CEEB;
    }
  </style>
</head>
<body>
  <canvas id="gameCanvas" width="800" height="400"></canvas>
  <script>
    const canvas = document.getElementById('gameCanvas');
    const ctx = canvas.getContext('2d');

    // Game variables
    const gravity = 0.5;
    const player = {
      x: 50,
      y: 300,
      width: 30,
      height: 30,
      color: 'red',
      dx: 0,
      dy: 0,
      speed: 5,
      jumpPower: -10,
      isOnGround: false
    };

    const platforms = [
      { x: 0, y: 350, width: 800, height: 50, color: 'green' },
      { x: 200, y: 250, width: 150, height: 20, color: 'brown' },
      { x: 400, y: 200, width: 150, height: 20, color: 'brown' }
    ];

    // Input handling
    const keys = {};
    window.addEventListener('keydown', (e) => keys[e.key] = true);
    window.addEventListener('keyup', (e) => keys[e.key] = false);

    // Game loop
    function update() {
      // Horizontal movement
      if (keys['ArrowRight'] || keys['d']) player.dx = player.speed;
      else if (keys['ArrowLeft'] || keys['a']) player.dx = -player.speed;
      else player.dx = 0;

      // Jumping
      if ((keys['ArrowUp'] || keys['w'] || keys[' ']) && player.isOnGround) {
        player.dy = player.jumpPower;
        player.isOnGround = false;
      }

      // Apply gravity
      player.dy += gravity;

      // Update player position
      player.x += player.dx;
      player.y += player.dy;

      // Collision detection with platforms
      player.isOnGround = false;
      platforms.forEach(platform => {
        if (player.x < platform.x + platform.width &&
            player.x + player.width > platform.x &&
            player.y + player.height > platform.y &&
            player.y + player.height < platform.y + platform.height) {
          player.dy = 0;
          player.y = platform.y - player.height;
          player.isOnGround = true;
        }
      });

      // Prevent player from falling off the canvas
      if (player.y > canvas.height) {
        player.y = canvas.height - player.height;
        player.dy = 0;
        player.isOnGround = true;
      }
    }

    function draw() {
      // Clear canvas
      ctx.clearRect(0, 0, canvas.width, canvas.height);

      // Draw player
      ctx.fillStyle = player.color;
      ctx.fillRect(player.x, player.y, player.width, player.height);

      // Draw platforms
      platforms.forEach(platform => {
        ctx.fillStyle = platform.color;
        ctx.fillRect(platform.x, platform.y, platform.width, platform.height);
      });
    }

    function gameLoop() {
      update();
      draw();
      requestAnimationFrame(gameLoop);
    }

    gameLoop();
  </script>
</body>
</html>
