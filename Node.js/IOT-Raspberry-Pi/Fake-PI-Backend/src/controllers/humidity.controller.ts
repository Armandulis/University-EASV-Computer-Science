import {
  Body,
  Controller,
  Get,
  Inject,
  Param,
  Put,
  Query,
} from '@nestjs/common';
import { Temperature } from '../models/temperature.schema';
import { HumidityService } from '../services/humidity.service';

/**
 * Controller HumidityController
 */
@Controller('humidity')
export class HumidityController {
  constructor(private readonly service: HumidityService) {}

  /**
   * GET request to /humidity?sensorId={sensorId}
   * @param query
   */
  @Get()
  get(@Query() query) {
    const sensorId = query.sensorId;
    return this.service.getSensorData(sensorId);
  }

  /**
   * GET request to /humidity
   * @param params
   */
  @Get(':latest')
  findLatest(@Param() params) {
    return this.service.getLatest();
  }

  /**
   * PUT request to /humidity/{sensorId}
   * @param dto
   */
  @Put()
  update(@Body() dto: Temperature): string {
    return this.service.sendSensorData('temperature', dto);
  }
}
