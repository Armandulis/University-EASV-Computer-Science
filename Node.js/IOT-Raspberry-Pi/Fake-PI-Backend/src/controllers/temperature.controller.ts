import { Body, Controller, Get, Param, Put, Query } from '@nestjs/common';
import { TemperatureService } from '../services/temperature.service';
import { Temperature } from '../models/temperature.schema';

/**
 * Controller TemperatureController
 */
@Controller('temperature')
export class TemperatureController {
  constructor(private readonly service: TemperatureService) {}

  /**
   * GET request to /temperature?sensorId={sensorId}
   * @param query
   */
  @Get()
  get(@Query() query) {
    const sensorId = query.sensorId;
    return this.service.getSensorData(sensorId);
  }

  /**
   * GET request to /temperature
   * @param params
   */
  @Get(':latest')
  findLatest(@Param() params) {
    return this.service.getLatest();
  }

  /**
   * PUT request to /temperature
   * @param dto
   */
  @Put()
  update(@Body() dto: Temperature): string {
    return this.service.sendSensorData('temperature', dto);
  }
}
