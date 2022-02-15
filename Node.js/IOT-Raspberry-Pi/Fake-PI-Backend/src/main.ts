import { NestFactory } from '@nestjs/core';
import { AppModule } from './app.module';
import { Transport } from '@nestjs/microservices';

async function bootstrap() {
  const app = await NestFactory.create(AppModule);
  app.enableCors({
    // origin: 'http://localhost:4200',
    // origin: 'https://iot-fake-pi-sensors.web.app',
    origin: '*',
  });
  app.connectMicroservice({
    transport: Transport.MQTT,
    options: {
      url: 'mqtts://driver.cloudmqtt.com:28692',
      username: 'guhpfdxf',
      password: 'jcceQ1eHsoAg',
    },
  });
  app.startAllMicroservices();
  await app.listen(process.env.PORT);
}

bootstrap();
