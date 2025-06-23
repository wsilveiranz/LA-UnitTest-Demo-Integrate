<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:math="http://www.w3.org/2005/xpath-functions/math" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:dm="http://azure.workflow.datamapper" xmlns:ef="http://azure.workflow.datamapper.extensions" xmlns="http://www.w3.org/2005/xpath-functions" exclude-result-prefixes="xsl xs math dm ef" version="3.0" expand-text="yes">
  <xsl:output indent="yes" media-type="text/json" method="text" omit-xml-declaration="yes" />
  <xsl:template match="/">
    <xsl:variable name="xmlinput" select="json-to-xml(/)" />
    <xsl:variable name="xmloutput">
      <xsl:apply-templates select="$xmlinput" mode="azure.workflow.datamapper" />
    </xsl:variable>
    <xsl:value-of select="xml-to-json($xmloutput,map{'indent':true()})" />
  </xsl:template>
  <xsl:template match="/" mode="azure.workflow.datamapper">
    <map>
      <string key="orderId">{concat('FC-', /*/*[@key='orderId'])}</string>
      <string key="customerId">{dm:if_then_else(starts-with(/*/*[@key='customerId'], 'X-'), replace(/*/*[@key='customerId'], 'X-', 'VIP-'), /*/*[@key='customerId'])}</string>
      <xsl:choose>
        <xsl:when test="local-name-from-QName(node-name(/*/*[@key='region'])) = 'null'">
          <null key="region" />
        </xsl:when>
        <xsl:otherwise>
          <string key="region">{/*/*[@key='region']}</string>
        </xsl:otherwise>
      </xsl:choose>
      <string key="status">{dm:if_then_else(exists(/*/*[@key='status']), upper-case(/*/*[@key='status']), 'NEW')}</string>
      <array key="orderDetails">
        <xsl:for-each select="/*/*[@key='orderDetails']/*">
          <map>
            <xsl:choose>
              <xsl:when test="local-name-from-QName(node-name(*[@key='productId'])) = 'null'">
                <null key="productId" />
              </xsl:when>
              <xsl:otherwise>
                <string key="productId">{*[@key='productId']}</string>
              </xsl:otherwise>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test="local-name-from-QName(node-name(*[@key='productName'])) = 'null'">
                <null key="productName" />
              </xsl:when>
              <xsl:otherwise>
                <string key="productName">{*[@key='productName']}</string>
              </xsl:otherwise>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test="local-name-from-QName(node-name(*[@key='quantity'])) = 'null'">
                <null key="quantity" />
              </xsl:when>
              <xsl:otherwise>
                <number key="quantity">{*[@key='quantity']}</number>
              </xsl:otherwise>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test="local-name-from-QName(node-name(*[@key='unitPrice'])) = 'null'">
                <null key="unitPrice" />
              </xsl:when>
              <xsl:otherwise>
                <number key="unitPrice">{*[@key='unitPrice']}</number>
              </xsl:otherwise>
            </xsl:choose>
          </map>
        </xsl:for-each>
      </array>
    </map>
  </xsl:template>
  <xsl:function name="dm:if_then_else" as="xs:string">
    <xsl:param name="condition" as="xs:boolean" />
    <xsl:param name="thenResult" as="xs:anyAtomicType?" />
    <xsl:param name="elseResult" as="xs:anyAtomicType?" />
    <xsl:choose>
      <xsl:when test="$condition">
        <xsl:value-of select="$thenResult" />
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$elseResult" />
      </xsl:otherwise>
    </xsl:choose>
  </xsl:function>
</xsl:stylesheet>