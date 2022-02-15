import {
  Body,
  Controller,
  Get,
  Param,
  Put,
} from '@nestjs/common';
import { SettingsService } from '../services/settings.service';
import { Settings } from '../models/settings.schema';

/**
 * Controller SettingsController
 */
@Controller('settings')
export class SettingsController{
  constructor(private readonly service: SettingsService) {}

  /**
   * GET request to /settings?sensorId={sensorId}
   * @param params
   */
  @Get(':latest')
  findLatest(@Param() params) {
    return this.service.getLatest();
  }

  /**
   * PUT request to /settings
   * @param dto
   */
  @Put()
  update(@Body() dto: Settings): string {
    console.log(dto);
    return this.service.sendNewSetting( dto );
  }
}
