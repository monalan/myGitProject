#!/usr/bin/python
# -*- coding: UTF-8 -*-

import base64


def handle_jpg_to_py():
    """
    将jpg图像文件转换为py文件
    :param picture_name:
    :return:
    """
    open_jpg = open("ship.bmp", 'rb')
    b64str = base64.b64encode(open_jpg.read())
    open_jpg.close()
    # 注意这边b64str一定要加上.decode()
    write_data = b64str.decode()
    f = open('imgstr.py', 'w+')
    f.write(write_data)
    f.close()


handle_jpg_to_py()
